using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace BlazorBindings.Avalonia.Navigation;

public class ObservableStack<T> : Stack<T>, INotifyCollectionChanged, INotifyPropertyChanged, IList
{
    public bool IsFixedSize => false;
    public bool IsReadOnly => false;

    public object this[int index]
    {
        get
        {
            return this.ElementAt(index);
        }
        set
        {
            throw new NotSupportedException();
        }
    }

    #region Constructors

    public ObservableStack() : base() { }

    public ObservableStack(IEnumerable<T> collection) : base(collection) { }

    public ObservableStack(int capacity) : base(capacity) { }

    #endregion

    #region Overrides

    public virtual new T Pop()
    {
        var item = base.Pop();
        OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
        
        return item;
    }

    public virtual new bool TryPop(out T item)
    {
        var result = base.TryPop(out item);
        OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);

        return result;
    }

    public virtual new void Push(T item)
    {
        base.Push(item);
        OnCollectionChanged(NotifyCollectionChangedAction.Add, item);
    }

    public virtual new void Clear()
    {
        base.Clear();
        OnCollectionChanged(NotifyCollectionChangedAction.Reset, default);
    }

    #endregion

    #region CollectionChanged

    public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

    protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T item)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(
            action
            , item
            , item == null ? -1 : 0)
        );

        OnPropertyChanged(nameof(Count));
    }

    #endregion

    #region PropertyChanged

    public virtual event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string proertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proertyName));
    }

    public int Add(object value)
    {
        throw new NotImplementedException();
    }

    public bool Contains(object value)
    {
        throw new NotImplementedException();
    }

    public int IndexOf(object value)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, object value)
    {
        throw new NotImplementedException();
    }

    public void Remove(object value)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    #endregion
}
