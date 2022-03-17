using System;
using System.Data.SqlTypes;

class MinHeap <T> where T : class
{

    // Member variables of this class
    private HeapElement<T>[] Heap;
    private int size;
    private int maxsize;

    // Initializing front as static with unity
    private int FRONT = 1;

    // Constructor of this class
    public MinHeap(int maxsize)
    {
        // This keyword refers to current object itself
        this.maxsize = maxsize;
        this.size = 0;

        Heap = new HeapElement<T>[this.maxsize + 1];
        Heap[0] = new HeapElement<T>(null, Int32.MinValue);
    }
    // Method 1
    // Returning the position of
    // the parent for the node currently
    // at pos
    private int parent(int pos) { return pos / 2; }

    // Method 2
    // Returning the position of the
    // left child for the node currently at pos
    private int leftChild(int pos) { return (2 * pos); }

    // Method 3
    // Returning the position of
    // the right child for the node currently
    // at pos
    private int rightChild(int pos)
    {
        return (2 * pos) + 1;
    }

    // Method 4
    // Returning true if the passed
    // node is a leaf node
    private bool isLeaf(int pos)
    {

        if (pos > (size / 2) && pos <= size) {
            return true;
        }

        return false;
    }

    // Method 5
    // To swap two nodes of the heap
    private void swap(int fpos, int spos)
    {

        HeapElement<T> tmp = Heap[fpos];

        Heap[fpos] = Heap[spos];
        Heap[spos] = tmp;
    }

    // Method 6
    // To heapify the node at pos
    private void minHeapify(int pos)
    {
        // FIXME : A lot of changes will be done here !
        // If the node is a non-leaf node and greater
        // than any of its child
        if (!isLeaf(pos)) {
            if (Heap[pos].Value > Heap[leftChild(pos)].Value
                || Heap[pos].Value > Heap[rightChild(pos)].Value) {

                // Swap with the left child and heapify
                // the left child
                if (Heap[leftChild(pos)].Value
                    < Heap[rightChild(pos)].Value) {
                    swap(pos, leftChild(pos));
                    minHeapify(leftChild(pos));
                }

                // Swap with the right child and heapify
                // the right child
                else {
                    swap(pos, rightChild(pos));
                    minHeapify(rightChild(pos));
                }
            }
        }
    }

    // Method 7
    // To insert a node into the heap
    public void Enqueue(HeapElement<T> element)
    {
        // FIXME : A lot of changes will be done here !

        if (size >= maxsize) {
            return;
        }

        Heap[++size] = element;
        int current = size;

        while (Heap[current].Value < Heap[parent(current)].Value) {
            swap(current, parent(current));
            current = parent(current);
        }
    }

    // Method 9
    // To remove and return the minimum
    // element from the heap
    public HeapElement<T> Dequeue()
    {
        HeapElement<T> popped = Heap[FRONT];
        Heap[FRONT] = Heap[size--];
        minHeapify(FRONT);

        return popped;
    }
}

public class HeapElement<T> where T : class
{
    private int value;
    public int Value => value;

    private T node;
    public T Node => node;

    public HeapElement(T node, int value)
    {
        this.node = node;
        this.value = value;
    }
}

