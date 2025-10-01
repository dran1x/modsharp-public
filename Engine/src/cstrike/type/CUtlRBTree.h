// This file modified from https://github.com/alliedmodders/hl2sdk/tree/cs2

#ifndef CSTRIKE_TYPE_CUTLRBTREE_H
#define CSTRIKE_TYPE_CUTLRBTREE_H

#include "cstrike/type/CUtlLeanVector.h"

template <typename T>
class CDefLess
{
public:
    CDefLess() {}
    CDefLess(int32_t i) {}
    inline bool operator()(const T& lhs, const T& rhs) const { return (lhs < rhs); }
    inline bool operator!() const { return false; }
};

enum CompareOperands_t
{
    k_EEqual                = 0x1,
    k_EGreaterThan          = 0x2,
    k_ELessThan             = 0x4,
    k_EGreaterThanOrEqualTo = k_EGreaterThan | k_EEqual,
    k_ELessThanOrEqualTo    = k_ELessThan | k_EEqual,
};

template <class I>
struct UtlRBTreeLinks_t
{
    I m_Left;
    I m_Right;
    I m_Parent;
    I m_Tag;
};

template <class T, class I>
struct UtlRBTreeNode_t : public UtlRBTreeLinks_t<I>
{
    T m_Data;
};

template <class T, class I = uint16_t, typename L = bool (*)(const T&, const T&), class M = CUtlLeanVector<UtlRBTreeNode_t<T, I>>>
class CUtlRBTree
{
public:
    using KeyType_t   = T;
    using ElemType_t  = T;
    using IndexType_t = I;

    // Less func typedef
    // Returns true if the first parameter is "less" than the second
    using LessFunc_t = L;

    explicit CUtlRBTree(int32_t growSize = 0, int32_t initSize = 0, const LessFunc_t& lessfunc = 0);
    explicit CUtlRBTree(const LessFunc_t& lessfunc);
    ~CUtlRBTree();

    CUtlRBTree<T, I, L, M>& operator=(const CUtlRBTree<T, I, L, M>& copyFrom) = delete;
    CUtlRBTree<T, I, L, M>& operator=(CUtlRBTree<T, I, L, M>&& moveFrom)      = delete;

    void EnsureCapacity(int32_t num);

    // gets particular elements
    [[nodiscard]] T&       Element(I i);
    [[nodiscard]] const T& Element(I i) const;
    [[nodiscard]] T&       operator[](I i);
    [[nodiscard]] const T& operator[](I i) const;

    // Gets the root
    [[nodiscard]] I Root() const;

    // Num elements
    [[nodiscard]] uint32_t Count() const;

    // Max "size" of the vector
    // it's not generally safe to iterate from index 0 to MaxElement()-1 (you could do this as a potential
    // iteration optimization, IF CUtlMemory is the allocator, and IF IsValidIndex() is tested for each element...
    //  but this should be implemented inside the CUtlRBTree iteration API, if anywhere)
    [[nodiscard]] I MaxElement() const;

    // Gets the children
    [[nodiscard]] I Parent(I i) const;
    [[nodiscard]] I LeftChild(I i) const;
    [[nodiscard]] I RightChild(I i) const;

    // Tests if a node is a left or right child
    [[nodiscard]] bool IsLeftChild(I i) const;
    [[nodiscard]] bool IsRightChild(I i) const;

    // Tests if root or leaf
    [[nodiscard]] bool IsRoot(I i) const;
    [[nodiscard]] bool IsLeaf(I i) const;

    // Checks if a node is valid and in the tree
    [[nodiscard]] bool IsValidIndex(I i) const;

    // Checks if the tree as a whole is valid
    [[nodiscard]] bool IsValid() const;

    // Invalid index
    [[nodiscard]] static I InvalidIndex();

    // returns the tree depth (not a very fast operation)
    [[nodiscard]] int32_t Depth(I node) const;
    [[nodiscard]] int32_t Depth() const;

    // Sets the less func
    void SetLessFunc(const LessFunc_t& func);

    // Allocation method
    [[nodiscard]] I NewNode(bool bConstructElement);

    // Insert method (inserts in order)
    // NOTE: the returned 'index' will be valid as long as the element remains in the tree
    //       (other elements being added/removed will not affect it)
    [[nodiscard]] I    Insert(const T& insert);
    void               Insert(const T* pArray, int32_t nItems);
    [[nodiscard]] I    InsertIfNotFound(const T& insert);
    [[nodiscard]] I    InsertIfNotFound(T&& moveInsert);

    // Find method
    [[nodiscard]] I Find(const T& search) const;

    // FindFirst method ( finds first inorder if there are duplicates )
    [[nodiscard]] I FindFirst(const T& search) const;

    // First element >= key
    [[nodiscard]] I FindClosest(const T& search, CompareOperands_t eFindCriteria) const;

    // Remove methods
    void RemoveAt(I i);
    bool Remove(const T& remove);
    void RemoveAll();
    void Purge();

    // Allocation, deletion
    void FreeNode(I i);

    // Iteration
    [[nodiscard]] I FirstInorder() const;
    [[nodiscard]] I NextInorder(I i) const;
    [[nodiscard]] I PrevInorder(I i) const;
    [[nodiscard]] I LastInorder() const;

    [[nodiscard]] I FirstPreorder() const;
    [[nodiscard]] I NextPreorder(I i) const;
    [[nodiscard]] I PrevPreorder(I i) const;
    [[nodiscard]] I LastPreorder() const;

    [[nodiscard]] I FirstPostorder() const;
    [[nodiscard]] I NextPostorder(I i) const;

    // If you change the search key, this can be used to reinsert the
    // element into the tree.
    void Reinsert(I elem);

protected:
    enum NodeColor_t
    {
        RED = 0,
        BLACK
    };

    using Node_t  = UtlRBTreeNode_t<T, I>;
    using Links_t = UtlRBTreeLinks_t<I>;

    // Sets the children
    void SetParent(I i, I parent);
    void SetLeftChild(I i, I child);
    void SetRightChild(I i, I child);
    void LinkToParent(I i, I parent, bool isLeft);

    // Gets at the links
    [[nodiscard]] const Links_t& Links(I i) const;
    [[nodiscard]] Links_t&       Links(I i);

    // Checks if a link is red or black
    [[nodiscard]] bool IsRed(I i) const;
    [[nodiscard]] bool IsBlack(I i) const;

    // Sets/gets node color
    [[nodiscard]] NodeColor_t Color(I i) const;
    void        SetColor(I i, NodeColor_t c);

    // operations required to preserve tree balance
    void RotateLeft(I i);
    void RotateRight(I i);
    void InsertRebalance(I i);
    void RemoveRebalance(I i);

    // Insertion, removal
    [[nodiscard]] I InsertAt(I parent, bool leftchild, bool bConstructElement);

    // Inserts a node into the tree, doesn't copy the data in.
    void FindInsertionPosition(const T& insert, I& parent, bool& leftchild);

    // Remove and add back an element in the tree.
    void Unlink(I elem);
    void Link(I elem);

    // Used for sorting.
    LessFunc_t m_LessFunc;

    M                      m_Elements;
    I                      m_Root;
    I                      m_NumElements;
    I                      m_FirstFree;
    typename M::Iterator_t m_LastAlloc; // the last index allocated

    const M& Elements() const
    {
        return m_Elements;
    }
};

template <class T, class I, typename L, class M>
inline CUtlRBTree<T, I, L, M>::CUtlRBTree(int32_t growSize, int32_t initSize, const LessFunc_t& lessfunc) :
    m_LessFunc(lessfunc),
    m_Elements(growSize, initSize),
    m_Root(InvalidIndex()),
    m_NumElements(0),
    m_FirstFree(InvalidIndex()),
    m_LastAlloc(m_Elements.InvalidIterator())
{
}

template <class T, class I, typename L, class M>
inline CUtlRBTree<T, I, L, M>::CUtlRBTree(const LessFunc_t& lessfunc) :
    m_LessFunc(lessfunc),
    m_Elements((I)0, (I)0),
    m_Root(InvalidIndex()),
    m_NumElements(0),
    m_FirstFree(InvalidIndex()),
    m_LastAlloc(m_Elements.InvalidIterator())
{
}

template <class T, class I, typename L, class M>
inline CUtlRBTree<T, I, L, M>::~CUtlRBTree()
{
    Purge();
}

template <class T, class I, typename L, class M>
inline void CUtlRBTree<T, I, L, M>::EnsureCapacity(int32_t num)
{
    m_Elements.EnsureCapacity(num);
}

template <class T, class I, typename L, class M>
inline T& CUtlRBTree<T, I, L, M>::Element(I i)
{
    return m_Elements[i].m_Data;
}

template <class T, class I, typename L, class M>
inline const T& CUtlRBTree<T, I, L, M>::Element(I i) const
{
    return m_Elements[i].m_Data;
}

template <class T, class I, typename L, class M>
inline T& CUtlRBTree<T, I, L, M>::operator[](I i)
{
    return Element(i);
}

template <class T, class I, typename L, class M>
inline const T& CUtlRBTree<T, I, L, M>::operator[](I i) const
{
    return Element(i);
}

template <class T, class I, typename L, class M>
inline I CUtlRBTree<T, I, L, M>::Root() const
{
    return m_Root;
}

//-----------------------------------------------------------------------------
// Num elements
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline uint32_t CUtlRBTree<T, I, L, M>::Count() const
{
    return static_cast<uint32_t>(m_NumElements);
}

//-----------------------------------------------------------------------------
// Max "size" of the vector
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline I CUtlRBTree<T, I, L, M>::MaxElement() const
{
    return static_cast<I>(m_Elements.NumAllocated());
}

//-----------------------------------------------------------------------------
// Gets the children
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline I CUtlRBTree<T, I, L, M>::Parent(I i) const
{
    return i != InvalidIndex() ? m_Elements[i].m_Parent : InvalidIndex();
}

template <class T, class I, typename L, class M>
inline I CUtlRBTree<T, I, L, M>::LeftChild(I i) const
{
    return i != InvalidIndex() ? m_Elements[i].m_Left : InvalidIndex();
}

template <class T, class I, typename L, class M>
inline I CUtlRBTree<T, I, L, M>::RightChild(I i) const
{
    return i != InvalidIndex() ? m_Elements[i].m_Right : InvalidIndex();
}

//-----------------------------------------------------------------------------
// Tests if a node is a left or right child
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline bool CUtlRBTree<T, I, L, M>::IsLeftChild(I i) const
{
    return LeftChild(Parent(i)) == i;
}

template <class T, class I, typename L, class M>
inline bool CUtlRBTree<T, I, L, M>::IsRightChild(I i) const
{
    return RightChild(Parent(i)) == i;
}

//-----------------------------------------------------------------------------
// Tests if root or leaf
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline bool CUtlRBTree<T, I, L, M>::IsRoot(I i) const
{
    return i == m_Root;
}

template <class T, class I, typename L, class M>
inline bool CUtlRBTree<T, I, L, M>::IsLeaf(I i) const
{
    return (LeftChild(i) == InvalidIndex()) && (RightChild(i) == InvalidIndex());
}

//-----------------------------------------------------------------------------
// Checks if a node is valid and in the tree
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline bool CUtlRBTree<T, I, L, M>::IsValidIndex(I i) const
{
    if (!m_Elements.IsIdxValid(i))
        return false;

    if (m_Elements.IsIdxAfter(i, m_LastAlloc))
        return false; // don't read values that have been allocated, but not constructed

    return LeftChild(i) != i;
}

//-----------------------------------------------------------------------------
// Invalid index
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline I CUtlRBTree<T, I, L, M>::InvalidIndex()
{
    return (I)M::InvalidIndex();
}

//-----------------------------------------------------------------------------
// returns the tree depth (not a very fast operation)
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline int32_t CUtlRBTree<T, I, L, M>::Depth() const
{
    return Depth(Root());
}

//-----------------------------------------------------------------------------
// Sets the children
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline void CUtlRBTree<T, I, L, M>::SetParent(I i, I parent)
{
    Links(i).m_Parent = parent;
}

template <class T, class I, typename L, class M>
inline void CUtlRBTree<T, I, L, M>::SetLeftChild(I i, I child)
{
    Links(i).m_Left = child;
}

template <class T, class I, typename L, class M>
inline void CUtlRBTree<T, I, L, M>::SetRightChild(I i, I child)
{
    Links(i).m_Right = child;
}

//-----------------------------------------------------------------------------
// Gets at the links
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline const typename CUtlRBTree<T, I, L, M>::Links_t& CUtlRBTree<T, I, L, M>::Links(I i) const
{
    // Sentinel node, makes life easier
    static const Links_t s_Sentinel =
        {
            // Use M::INVALID_INDEX instead of InvalidIndex() so that this is
            // a compile-time constant -- otherwise it is constructed on the first
            // call!
            M::INVALID_INDEX, M::INVALID_INDEX, M::INVALID_INDEX, CUtlRBTree<T, I, L, M>::BLACK};

    return (i != InvalidIndex()) ? m_Elements[i] : s_Sentinel;
}

template <class T, class I, typename L, class M>
inline typename CUtlRBTree<T, I, L, M>::Links_t& CUtlRBTree<T, I, L, M>::Links(I i)
{
    return m_Elements[i];
}

//-----------------------------------------------------------------------------
// Checks if a link is red or black
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline bool CUtlRBTree<T, I, L, M>::IsRed(I i) const
{
    return Color(i) == RED;
}

template <class T, class I, typename L, class M>
inline bool CUtlRBTree<T, I, L, M>::IsBlack(I i) const
{
    return Color(i) == BLACK;
}

//-----------------------------------------------------------------------------
// Sets/gets node color
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
inline typename CUtlRBTree<T, I, L, M>::NodeColor_t CUtlRBTree<T, I, L, M>::Color(I i) const
{
    return (NodeColor_t)(i != InvalidIndex() ? m_Elements[i].m_Tag : BLACK);
}

template <class T, class I, typename L, class M>
inline void CUtlRBTree<T, I, L, M>::SetColor(I i, typename CUtlRBTree<T, I, L, M>::NodeColor_t c)
{
    Links(i).m_Tag = (I)c;
}

//-----------------------------------------------------------------------------
// Allocates/ deallocates nodes
//-----------------------------------------------------------------------------
#ifdef _MSC_VER
#    pragma warning(push)
#    pragma warning(disable : 4389) // '==' : signed/unsigned mismatch
#endif

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::NewNode(bool bConstructElement)
{
    I elem;

    // Nothing in the free list; add.
    if (m_FirstFree == InvalidIndex())
    {
        m_LastAlloc = m_Elements.AddToTail();

        elem = m_Elements.GetIndex(m_LastAlloc);
    }
    else
    {
        elem        = m_FirstFree;
        m_FirstFree = RightChild(m_FirstFree);
    }

    if (bConstructElement)
        std::construct_at(&Element(elem));

    return elem;
}

#ifdef _MSC_VER
#    pragma warning(pop)
#endif

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::FreeNode(I i)
{
    std::destroy_at(&Element(i));
    SetLeftChild(i, i); // indicates it's in not in the tree
    SetRightChild(i, m_FirstFree);
    m_FirstFree = i;
}

//-----------------------------------------------------------------------------
// Rotates node i to the left
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::RotateLeft(I elem)
{
    I rightchild = RightChild(elem);
    SetRightChild(elem, LeftChild(rightchild));
    if (LeftChild(rightchild) != InvalidIndex())
        SetParent(LeftChild(rightchild), elem);

    if (rightchild != InvalidIndex())
        SetParent(rightchild, Parent(elem));
    if (!IsRoot(elem))
    {
        if (IsLeftChild(elem))
            SetLeftChild(Parent(elem), rightchild);
        else
            SetRightChild(Parent(elem), rightchild);
    }
    else
        m_Root = rightchild;

    SetLeftChild(rightchild, elem);
    if (elem != InvalidIndex())
        SetParent(elem, rightchild);
}

//-----------------------------------------------------------------------------
// Rotates node i to the right
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::RotateRight(I elem)
{
    I leftchild = LeftChild(elem);
    SetLeftChild(elem, RightChild(leftchild));
    if (RightChild(leftchild) != InvalidIndex())
        SetParent(RightChild(leftchild), elem);

    if (leftchild != InvalidIndex())
        SetParent(leftchild, Parent(elem));
    if (!IsRoot(elem))
    {
        if (IsRightChild(elem))
            SetRightChild(Parent(elem), leftchild);
        else
            SetLeftChild(Parent(elem), leftchild);
    }
    else
        m_Root = leftchild;

    SetRightChild(leftchild, elem);
    if (elem != InvalidIndex())
        SetParent(elem, leftchild);
}

//-----------------------------------------------------------------------------
// Rebalances the tree after an insertion
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::InsertRebalance(I elem)
{
    while (!IsRoot(elem) && (Color(Parent(elem)) == RED))
    {
        I parent      = Parent(elem);
        I grandparent = Parent(parent);

        /* we have a violation */
        if (IsLeftChild(parent))
        {
            I uncle = RightChild(grandparent);
            if (IsRed(uncle))
            {
                /* uncle is RED */
                SetColor(parent, BLACK);
                SetColor(uncle, BLACK);
                SetColor(grandparent, RED);
                elem = grandparent;
            }
            else
            {
                /* uncle is BLACK */
                if (IsRightChild(elem))
                {
                    /* make x a left child, will change parent and grandparent */
                    elem = parent;
                    RotateLeft(elem);
                    parent      = Parent(elem);
                    grandparent = Parent(parent);
                }
                /* recolor and rotate */
                SetColor(parent, BLACK);
                SetColor(grandparent, RED);
                RotateRight(grandparent);
            }
        }
        else
        {
            /* mirror image of above code */
            I uncle = LeftChild(grandparent);
            if (IsRed(uncle))
            {
                /* uncle is RED */
                SetColor(parent, BLACK);
                SetColor(uncle, BLACK);
                SetColor(grandparent, RED);
                elem = grandparent;
            }
            else
            {
                /* uncle is BLACK */
                if (IsLeftChild(elem))
                {
                    /* make x a right child, will change parent and grandparent */
                    elem = parent;
                    RotateRight(parent);
                    parent      = Parent(elem);
                    grandparent = Parent(parent);
                }
                /* recolor and rotate */
                SetColor(parent, BLACK);
                SetColor(grandparent, RED);
                RotateLeft(grandparent);
            }
        }
    }
    SetColor(m_Root, BLACK);
}

//-----------------------------------------------------------------------------
// Insert a node into the tree
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::InsertAt(I parent, bool leftchild, bool bConstructElement)
{
    I i = NewNode(bConstructElement);
    LinkToParent(i, parent, leftchild);
    ++m_NumElements;

    return i;
}

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::LinkToParent(I i, I parent, bool isLeft)
{
    Links_t& elem = Links(i);
    elem.m_Parent = parent;
    elem.m_Left = elem.m_Right = InvalidIndex();
    elem.m_Tag                 = RED;

    /* insert node in tree */
    if (parent != InvalidIndex())
    {
        if (isLeft)
            Links(parent).m_Left = i;
        else
            Links(parent).m_Right = i;
    }
    else
    {
        m_Root = i;
    }

    InsertRebalance(i);
}

//-----------------------------------------------------------------------------
// Rebalance the tree after a deletion
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::RemoveRebalance(I elem)
{
    while (elem != m_Root && IsBlack(elem))
    {
        I parent = Parent(elem);

        // If elem is the left child of the parent
        if (elem == LeftChild(parent))
        {
            // Get our sibling
            I sibling = RightChild(parent);
            if (IsRed(sibling))
            {
                SetColor(sibling, BLACK);
                SetColor(parent, RED);
                RotateLeft(parent);

                // We may have a new parent now
                parent  = Parent(elem);
                sibling = RightChild(parent);
            }
            if ((IsBlack(LeftChild(sibling))) && (IsBlack(RightChild(sibling))))
            {
                if (sibling != InvalidIndex())
                    SetColor(sibling, RED);
                elem = parent;
            }
            else
            {
                if (IsBlack(RightChild(sibling)))
                {
                    SetColor(LeftChild(sibling), BLACK);
                    SetColor(sibling, RED);
                    RotateRight(sibling);

                    // rotation may have changed this
                    parent  = Parent(elem);
                    sibling = RightChild(parent);
                }
                SetColor(sibling, Color(parent));
                SetColor(parent, BLACK);
                SetColor(RightChild(sibling), BLACK);
                RotateLeft(parent);
                elem = m_Root;
            }
        }
        else
        {
            // Elem is the right child of the parent
            I sibling = LeftChild(parent);
            if (IsRed(sibling))
            {
                SetColor(sibling, BLACK);
                SetColor(parent, RED);
                RotateRight(parent);

                // We may have a new parent now
                parent  = Parent(elem);
                sibling = LeftChild(parent);
            }
            if ((IsBlack(RightChild(sibling))) && (IsBlack(LeftChild(sibling))))
            {
                if (sibling != InvalidIndex())
                    SetColor(sibling, RED);
                elem = parent;
            }
            else
            {
                if (IsBlack(LeftChild(sibling)))
                {
                    SetColor(RightChild(sibling), BLACK);
                    SetColor(sibling, RED);
                    RotateLeft(sibling);

                    // rotation may have changed this
                    parent  = Parent(elem);
                    sibling = LeftChild(parent);
                }
                SetColor(sibling, Color(parent));
                SetColor(parent, BLACK);
                SetColor(LeftChild(sibling), BLACK);
                RotateRight(parent);
                elem = m_Root;
            }
        }
    }
    SetColor(elem, BLACK);
}

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::Unlink(I elem)
{
    if (elem != InvalidIndex())
    {
        I x, y;

        if ((LeftChild(elem) == InvalidIndex()) || (RightChild(elem) == InvalidIndex()))
        {
            /* y has a NIL node as a child */
            y = elem;
        }
        else
        {
            /* find tree successor with a NIL node as a child */
            y = RightChild(elem);
            while (LeftChild(y) != InvalidIndex())
                y = LeftChild(y);
        }

        /* x is y's only child */
        if (LeftChild(y) != InvalidIndex())
            x = LeftChild(y);
        else
            x = RightChild(y);

        /* remove y from the parent chain */
        if (x != InvalidIndex())
            SetParent(x, Parent(y));
        if (!IsRoot(y))
        {
            if (IsLeftChild(y))
                SetLeftChild(Parent(y), x);
            else
                SetRightChild(Parent(y), x);
        }
        else
            m_Root = x;

        // need to store this off now, we'll be resetting y's color
        NodeColor_t ycolor = Color(y);
        if (y != elem)
        {
            // Standard implementations copy the data around, we cannot here.
            // Hook in y to link to the same stuff elem used to.
            SetParent(y, Parent(elem));
            SetRightChild(y, RightChild(elem));
            SetLeftChild(y, LeftChild(elem));

            if (!IsRoot(elem))
                if (IsLeftChild(elem))
                    SetLeftChild(Parent(elem), y);
                else
                    SetRightChild(Parent(elem), y);
            else
                m_Root = y;

            if (LeftChild(y) != InvalidIndex())
                SetParent(LeftChild(y), y);
            if (RightChild(y) != InvalidIndex())
                SetParent(RightChild(y), y);

            SetColor(y, Color(elem));
        }

        if ((x != InvalidIndex()) && (ycolor == BLACK))
            RemoveRebalance(x);
    }
}

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::Link(I elem)
{
    if (elem != InvalidIndex())
    {
        I    parent    = InvalidIndex();
        bool leftchild = false;

        FindInsertionPosition(Element(elem), parent, leftchild);

        LinkToParent(elem, parent, leftchild);
    }
}

//-----------------------------------------------------------------------------
// Delete a node from the tree
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::RemoveAt(I elem)
{
    if (elem != InvalidIndex())
    {
        Unlink(elem);

        FreeNode(elem);
        --m_NumElements;
    }
}

//-----------------------------------------------------------------------------
// remove a node in the tree
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
bool CUtlRBTree<T, I, L, M>::Remove(const T& search)
{
    I node = Find(search);
    if (node != InvalidIndex())
    {
        RemoveAt(node);
        return true;
    }
    return false;
}

//-----------------------------------------------------------------------------
// Removes all nodes from the tree
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::RemoveAll()
{
    // Have to do some convoluted stuff to invoke the destructor on all
    // valid elements for the multilist case (since we don't have all elements
    // connected to each other in a list).

    if (m_LastAlloc == m_Elements.InvalidIterator())
    {
        return;
    }

    for (typename M::Iterator_t it = m_Elements.First(); it != m_Elements.InvalidIterator(); it = m_Elements.Next(it))
    {
        I i = m_Elements.GetIndex(it);
        if (IsValidIndex(i)) // skip elements in the free list
        {
            std::destroy_at(&Element(i));
            SetRightChild(i, m_FirstFree);
            SetLeftChild(i, i);
            m_FirstFree = i;
        }

        if (it == m_LastAlloc)
            break; // don't destruct elements that haven't ever been constucted
    }

    // Clear everything else out
    m_Root        = InvalidIndex();
    m_NumElements = 0;
}

//-----------------------------------------------------------------------------
// Removes all nodes from the tree and purges memory
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::Purge()
{
    RemoveAll();
    m_FirstFree = InvalidIndex();
    m_Elements.Purge();
    m_LastAlloc = m_Elements.InvalidIterator();
}

//-----------------------------------------------------------------------------
// iteration
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::FirstInorder() const
{
    I i = m_Root;
    I left;
    while ((left = LeftChild(i)) != InvalidIndex())
        i = left;
    return i;
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::NextInorder(I i) const
{
    I right;
    if ((right = RightChild(i)) != InvalidIndex())
    {
        i = right;
        I left;
        while ((left = LeftChild(i)) != InvalidIndex())
            i = left;
        return i;
    }

    I parent = Parent(i);
    while (IsRightChild(i))
    {
        i = parent;
        if (i == InvalidIndex()) break;
        parent = Parent(i);
    }
    return parent;
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::PrevInorder(I i) const
{
    I left, right;
    if ((left = LeftChild(i)) != InvalidIndex())
    {
        i = left;
        while ((right = RightChild(i)) != InvalidIndex())
            i = right;
        return i;
    }

    I parent = Parent(i);
    while (IsLeftChild(i))
    {
        i = parent;
        if (i == InvalidIndex()) break;
        parent = Parent(i);
    }
    return parent;
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::LastInorder() const
{
    I i = m_Root;
    I right;
    while ((right = RightChild(i)) != InvalidIndex())
        i = right;
    return i;
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::FirstPreorder() const
{
    return m_Root;
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::NextPreorder(I i) const
{
    I left, right;
    if ((left = LeftChild(i)) != InvalidIndex())
        return left;

    if ((right = RightChild(i)) != InvalidIndex())
        return right;

    I parent = Parent(i);
    while (parent != InvalidIndex())
    {
        if (IsLeftChild(i) && (RightChild(parent) != InvalidIndex()))
            return RightChild(parent);
        i      = parent;
        parent = Parent(parent);
    }
    return InvalidIndex();
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::PrevPreorder(I i) const
{
    return InvalidIndex();
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::LastPreorder() const
{
    I i = m_Root;
    while (1)
    {
        I left, right;
        while ((right = RightChild(i)) != InvalidIndex())
            i = right;

        if ((left = LeftChild(i)) != InvalidIndex())
            i = left;
        else
            break;
    }
    return i;
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::FirstPostorder() const
{
    I i = m_Root;
    while (!IsLeaf(i))
    {
        I left;
        if ((left = LeftChild(i)) != InvalidIndex())
            i = left;
        else
            i = RightChild(i);
    }
    return i;
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::NextPostorder(I i) const
{
    I parent = Parent(i);
    if (parent == InvalidIndex())
        return InvalidIndex();

    if (IsRightChild(i))
        return parent;

    if (RightChild(parent) == InvalidIndex())
        return parent;

    i = RightChild(parent);
    while (!IsLeaf(i))
    {
        I left;
        if ((left = LeftChild(i)) != InvalidIndex())
            i = left;
        else
            i = RightChild(i);
    }
    return i;
}

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::Reinsert(I elem)
{
    Unlink(elem);
    Link(elem);
}

//-----------------------------------------------------------------------------
// returns the tree depth (not a very fast operation)
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
int32_t CUtlRBTree<T, I, L, M>::Depth(I node) const
{
    if (node == InvalidIndex())
        return 0;

    int32_t depthright = Depth(RightChild(node));
    int32_t depthleft  = Depth(LeftChild(node));
    return std::max(depthright, depthleft) + 1;
}

// #define UTLTREE_PARANOID

//-----------------------------------------------------------------------------
// Makes sure the tree is valid after every operation
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
bool CUtlRBTree<T, I, L, M>::IsValid() const
{
    if (!Count())
        return true;

    if (m_LastAlloc == m_Elements.InvalidIterator())
        return false;

    if (!m_Elements.IsIdxValid(Root()))
        return false;

    if (Parent(Root()) != InvalidIndex())
        return false;

#ifdef UTLTREE_PARANOID

    // First check to see that mNumEntries matches reality.
    // count items on the free list
    int32_t numFree = 0;
    for (int32_t i = m_FirstFree; i != InvalidIndex(); i = RightChild(i))
    {
        ++numFree;
        if (!m_Elements.IsIdxValid(i))
            return false;
    }

    // iterate over all elements, looking for validity
    // based on the self pointers
    int32_t nElements = 0;
    int32_t numFree2  = 0;
    for (M::Iterator_t it = m_Elements.First(); it != m_Elements.InvalidIterator(); it = m_Elements.Next(it))
    {
        I i = m_Elements.GetIndex(it);
        if (!IsValidIndex(i))
        {
            ++numFree2;
        }
        else
        {
            ++nElements;

            int32_t right = RightChild(i);
            int32_t left  = LeftChild(i);
            if ((right == left) && (right != InvalidIndex()))
                return false;

            if (right != InvalidIndex())
            {
                if (!IsValidIndex(right))
                    return false;
                if (Parent(right) != i)
                    return false;
                if (IsRed(i) && IsRed(right))
                    return false;
            }

            if (left != InvalidIndex())
            {
                if (!IsValidIndex(left))
                    return false;
                if (Parent(left) != i)
                    return false;
                if (IsRed(i) && IsRed(left))
                    return false;
            }
        }

        if (it == m_LastAlloc)
            break;
    }
    if (numFree2 != numFree)
        return false;

    if (nElements != m_NumElements)
        return false;

#endif // UTLTREE_PARANOID

    return true;
}

//-----------------------------------------------------------------------------
// Sets the less func
//-----------------------------------------------------------------------------

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::SetLessFunc(const typename CUtlRBTree<T, I, L, M>::LessFunc_t& func)
{
    m_LessFunc = func;
}

//-----------------------------------------------------------------------------
// inserts a node into the tree
//-----------------------------------------------------------------------------

// Inserts a node into the tree, doesn't copy the data in.
template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::FindInsertionPosition(const T& insert, I& parent, bool& leftchild)
{
    /* find where node belongs */
    I current = m_Root;
    parent    = InvalidIndex();
    leftchild = false;
    while (current != InvalidIndex())
    {
        parent = current;
        if (m_LessFunc(insert, Element(current)))
        {
            leftchild = true;
            current   = LeftChild(current);
        }
        else
        {
            leftchild = false;
            current   = RightChild(current);
        }
    }
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::Insert(const T& insert)
{
    // use copy constructor to copy it in
    I    parent    = InvalidIndex();
    bool leftchild = false;
    FindInsertionPosition(insert, parent, leftchild);
    I newNode = InsertAt(parent, leftchild, false);
    std::construct_at(&Element(newNode), insert);
    return newNode;
}

template <class T, class I, typename L, class M>
void CUtlRBTree<T, I, L, M>::Insert(const T* pArray, int32_t nItems)
{
    while (nItems--)
    {
        Insert(*pArray++);
    }
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::InsertIfNotFound(const T& insert)
{
    // use copy constructor to copy it in
    I    parent;

    I current = m_Root;
    parent    = InvalidIndex();
    bool leftchild = false;
    while (current != InvalidIndex())
    {
        parent = current;
        if (m_LessFunc(insert, Element(current)))
        {
            leftchild = true;
            current   = LeftChild(current);
        }
        else if (m_LessFunc(Element(current), insert))
        {
            leftchild = false;
            current   = RightChild(current);
        }
        else
            // Match found, no insertion
            return InvalidIndex();
    }

    I newNode = InsertAt(parent, leftchild, false);
    std::construct_at(&Element(newNode), insert);
    return newNode;
}

template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::InsertIfNotFound(T&& moveInsert)
{
    // use copy constructor to copy it in
    I    parent;

    I current = m_Root;
    parent    = InvalidIndex();
    bool leftchild = false;
    while (current != InvalidIndex())
    {
        parent = current;
        // Move unreferences:
        if (m_LessFunc(moveInsert, Element(current)))
        {
            leftchild = true;
            current   = LeftChild(current);
        }
        else if (m_LessFunc(Element(current), moveInsert))
        {
            leftchild = false;
            current   = RightChild(current);
        }
        else
        {
            // Match found, no insertion
            return InvalidIndex();
        }
    }

    I newNode = InsertAt(parent, leftchild, false);
    std::construct_at(&Element(newNode), std::move(moveInsert));
    return newNode;
}

//-----------------------------------------------------------------------------
// finds a node in the tree
//-----------------------------------------------------------------------------
template <class T, class I, typename L, class M>
I CUtlRBTree<T, I, L, M>::Find(const T& search) const
{
    I current = m_Root;
    while (current != InvalidIndex())
    {
        if (m_LessFunc(search, Element(current)))
            current = LeftChild(current);
        else if (m_LessFunc(Element(current), search))
            current = RightChild(current);
        else
            break;
    }
    return current;
}

//-----------------------------------------------------------------------------
// finds a the first node (inorder) with this key in the tree
//-----------------------------------------------------------------------------
template <class T, class I, typename L, class E>
I CUtlRBTree<T, I, L, E>::FindFirst(const T& search) const
{
    I current = m_Root;
    I best    = InvalidIndex();
    while (current != InvalidIndex())
    {
        if (m_LessFunc(search, Element(current)))
            current = LeftChild(current);
        else if (m_LessFunc(Element(current), search))
            current = RightChild(current);
        else
        {
            best    = current;
            current = LeftChild(current);
        }
    }
    return best;
}

//-----------------------------------------------------------------------------
// finds the closest node to the key supplied
//-----------------------------------------------------------------------------
template <class T, class I, typename L, class E>
I CUtlRBTree<T, I, L, E>::FindClosest(const T& search, CompareOperands_t eFindCriteria) const
{
    I current = m_Root;
    I best    = InvalidIndex();

    while (current != InvalidIndex())
    {
        if (m_LessFunc(search, Element(current)))
        {
            // current node is > key
            if (eFindCriteria & k_EGreaterThan)
                best = current;
            current = LeftChild(current);
        }
        else if (m_LessFunc(Element(current), search))
        {
            // current node is < key
            if (eFindCriteria & k_ELessThan)
                best = current;
            current = RightChild(current);
        }
        else
        {
            // exact match
            if (eFindCriteria & k_EEqual)
            {
                best = current;
                break;
            }
            if (eFindCriteria & k_EGreaterThan)
            {
                current = RightChild(current);
            }
            else if (eFindCriteria & k_ELessThan)
            {
                current = LeftChild(current);
            }
        }
    }
    return best;
}

#endif