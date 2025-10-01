// This file modified from https://github.com/alliedmodders/hl2sdk/tree/cs2

#ifndef CSTRIKE_TYPE_CUTLMAP_H
#define CSTRIKE_TYPE_CUTLMAP_H

#include "cstrike/type/CUtlRBTree.h"

template <typename K, typename T, typename I = uint16_t, typename LessFunc_t = bool (*)(const K&, const K&)>
class CUtlMap
{
public:
    using KeyType_t   = K;
    using ElemType_t  = T;
    using IndexType_t = I;

    struct Node_t
    {
        KeyType_t  key;
        ElemType_t elem;

        Node_t() :
            key(), elem() {}

        Node_t(const KeyType_t& inKey) :
            key(inKey), elem() {}
        Node_t(KeyType_t&& inKey) :
            key(std::move(inKey)), elem() {}

        Node_t(const KeyType_t& inKey, const ElemType_t& inElem) :
            key(inKey), elem(inElem) {}
        Node_t(const KeyType_t& inKey, ElemType_t&& moveElem) :
            key(inKey), elem(std::move(moveElem)) {}
        Node_t(KeyType_t&& moveKey, const ElemType_t& inElem) :
            key(std::move(moveKey)), elem(inElem) {}
        Node_t(KeyType_t&& moveKey, ElemType_t&& moveElem) :
            key(std::move(moveKey)), elem(std::move(moveElem)) {}

        Node_t(const Node_t& copyFrom) :
            Node_t(copyFrom.key, copyFrom.elem) {}
        Node_t(Node_t&& moveFrom) noexcept :
            Node_t(std::move(moveFrom.key), std::move(moveFrom.elem)) {}

        Node_t& operator=(const Node_t& copyFrom) = default;
        Node_t& operator=(Node_t&& moveFrom) noexcept
        {
            key  = std::move(moveFrom.key);
            elem = std::move(moveFrom.elem);
            return *this;
        }
    };

    class CKeyLess
    {
    public:
        CKeyLess(const LessFunc_t& lessFunc) :
            m_LessFunc(lessFunc) {}

        bool operator!() const
        {
            return !m_LessFunc;
        }

        bool operator()(const Node_t& left, const Node_t& right) const
        {
            return m_LessFunc(left.key, right.key);
        }

        LessFunc_t m_LessFunc;
    };

    using CTree = CUtlRBTree<Node_t, I, CKeyLess>;

    CUtlMap(int32_t growSize = 0, int32_t initSize = 0, const LessFunc_t& lessFunc = 0) :
        m_Tree(growSize, initSize, CKeyLess(lessFunc))
    {
    }

    CUtlMap(LessFunc_t lessFunc) :
        m_Tree(CKeyLess(lessFunc))
    {
    }

    void EnsureCapacity(int32_t num) { m_Tree.EnsureCapacity(num); }

    [[nodiscard]] ElemType_t&       Element(IndexType_t i) { return m_Tree.Element(i).elem; }
    [[nodiscard]] const ElemType_t& Element(IndexType_t i) const { return m_Tree.Element(i).elem; }
    [[nodiscard]] ElemType_t&       operator[](IndexType_t i) { return m_Tree.Element(i).elem; }
    [[nodiscard]] const ElemType_t& operator[](IndexType_t i) const { return m_Tree.Element(i).elem; }
    [[nodiscard]] KeyType_t&        Key(IndexType_t i) { return m_Tree.Element(i).key; }
    [[nodiscard]] const KeyType_t&  Key(IndexType_t i) const { return m_Tree.Element(i).key; }

    [[nodiscard]] uint32_t    Count() const { return m_Tree.Count(); }
    [[nodiscard]] IndexType_t MaxElement() const { return m_Tree.MaxElement(); }
    [[nodiscard]] bool        IsValidIndex(IndexType_t i) const { return m_Tree.IsValidIndex(i); }
    [[nodiscard]] bool        IsValid() const { return m_Tree.IsValid(); }

    // Invalid index
    [[nodiscard]] static IndexType_t InvalidIndex() { return CTree::InvalidIndex(); }

    // Sets the less func
    void SetLessFunc(LessFunc_t func) { m_Tree.SetLessFunc(CKeyLess(func)); }

    // Insert method (inserts in order)
    [[nodiscard]] IndexType_t Insert(const KeyType_t& key, const ElemType_t& insert) { return m_Tree.Insert(Node_t(key, insert)); }
    [[nodiscard]] IndexType_t Insert(const KeyType_t& key, ElemType_t&& insert) { return m_Tree.Insert(Node_t(key, std::move(insert))); }
    [[nodiscard]] IndexType_t Insert(KeyType_t&& key, const ElemType_t& insert) { return m_Tree.Insert(Node_t(std::move(key), insert)); }
    [[nodiscard]] IndexType_t Insert(KeyType_t&& key, ElemType_t&& insert) { return m_Tree.Insert(Node_t(std::move(key), std::move(insert))); }
    [[nodiscard]] IndexType_t Insert(const KeyType_t& key) { return m_Tree.Insert(Node_t(key)); }
    [[nodiscard]] IndexType_t InsertWithDupes(const KeyType_t& key, const ElemType_t& insert) { return m_Tree.Insert(Node_t(key, insert)); }
    [[nodiscard]] IndexType_t InsertWithDupes(const KeyType_t& key) { return m_Tree.Insert(Node_t(key)); }

    [[nodiscard]] bool HasElement(const KeyType_t& key) const { return m_Tree.HasElement(Node_t(key)); }

    [[nodiscard]] IndexType_t       Find(const KeyType_t& key) const { return m_Tree.Find(Node_t(key)); }
    [[nodiscard]] IndexType_t       FindFirst(const KeyType_t& key) const { return m_Tree.FindFirst(Node_t(key)); }
    [[nodiscard]] const ElemType_t& FindElement(const KeyType_t& key, const ElemType_t& defaultValue) const
    {
        IndexType_t i = Find(key);
        if (i == InvalidIndex())
            return defaultValue;
        return Element(i);
    }
    [[nodiscard]] IndexType_t FindClosest(const KeyType_t& key, CompareOperands_t eFindCriteria) const
    {
        Node_t dummyNode;
        dummyNode.key = key;
        return m_Tree.FindClosest(dummyNode, eFindCriteria);
    }

    // Remove methods
    void RemoveAt(IndexType_t i) { m_Tree.RemoveAt(i); }
    bool Remove(const KeyType_t& key)
    {
        Node_t dummyNode;
        dummyNode.key = key;
        return m_Tree.Remove(dummyNode);
    }

    void RemoveAll() { m_Tree.RemoveAll(); }
    void Purge() { m_Tree.Purge(); }

    // Purges the list and calls delete on each element in it.
    void PurgeAndDeleteElements()
    {
        for (IndexType_t i = 0; i < MaxElement(); ++i)
        {
            if (!IsValidIndex(i))
                continue;

            delete Element(i);
        }

        Purge();
    }

    // Iteration
    [[nodiscard]] IndexType_t FirstInorder() const { return m_Tree.FirstInorder(); }
    [[nodiscard]] IndexType_t NextInorder(IndexType_t i) const { return m_Tree.NextInorder(i); }
    [[nodiscard]] IndexType_t PrevInorder(IndexType_t i) const { return m_Tree.PrevInorder(i); }
    [[nodiscard]] IndexType_t LastInorder() const { return m_Tree.LastInorder(); }
    [[nodiscard]] IndexType_t InsertOrReplace(const KeyType_t& key, const ElemType_t& insert)
    {
        IndexType_t i = Find(key);
        if (i != InvalidIndex())
        {
            Element(i) = insert;
            return i;
        }

        return Insert(key, insert);
    }

    [[nodiscard]] IndexType_t InsertOrReplace(const KeyType_t& key, ElemType_t&& moveInsert)
    {
        IndexType_t i = Find(key);
        if (i != InvalidIndex())
        {
            Element(i) = std::move(moveInsert);
            return i;
        }

        return Insert(key, std::move(moveInsert));
    }

protected:
    CTree m_Tree;
};

template <typename K, typename T, typename I = int32_t, typename LF = CDefLess<K>>
struct CUtlOrderedMap : public CUtlMap<K, T, I, LF>
{
};

#endif