using System.DataStructures;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>
	/// This class represents the Event Handler Dictionary
	/// </summary>
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class EventHandlerDictionary : Disposable
	{
		private readonly AvlTree<Entry> _handlers2 = new AvlTree<Entry>();

		/// <summary>
		/// Gets or sets the <see cref="System.Delegate"/> with the specified key.
		/// </summary>
		/// <value>The delegate</value>
		public Delegate this[string key]
		{
			get
			{
				AvlTreeNode<Entry> node = _handlers2.FindNode(new Entry(key));
				return (node != null) ? node.Value.Handler : null;
			}
			set
			{
				Entry entry = new Entry(key);
				AvlTreeNode<Entry> node = _handlers2.FindNode(entry);
				if (node != null)
				{
					node.Value.Handler = value;
				}
				else
				{
					entry.Handler = value;
					_handlers2.Add(entry);
				}
			}
		}

		/// <summary>
		/// Adds the handler.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public void AddHandler(string key, Delegate value)
		{
			Entry entry = new Entry(key);
			AvlTreeNode<Entry> node = _handlers2.FindNode(entry);
			if (node != null)
			{
				node.Value.Handler = Delegate.Combine(node.Value.Handler, value);
			}
			else
			{
				entry.Handler = value;
				_handlers2.Add(entry);
			}
		}

		/// <summary>
		/// Clears Event Handler Dictionary instance.
		/// </summary>
		public void Clear()
		{
			_handlers2.Clear();
		}

		/// <summary>
		/// Raises the specified event handler associated with the key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="sender">The sender.</param>
		public void Raise(string key, object sender)
		{
			AvlTreeNode<Entry> node = _handlers2.FindNode(new Entry(key));
			if (node == null)
			{
				return;
			}
			EventHandler handler = node.Value.Handler as EventHandler;
			if (handler != null)
			{
				handler(sender, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Raises the specified event handler along with event arguments associated with  the key.
		/// </summary>
		/// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
		/// <param name="key">The key.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The instance containing the event data.</param>
		public void Raise<TEventArgs>(string key, object sender, TEventArgs args) where TEventArgs : EventArgs
		{
			AvlTreeNode<Entry> node = _handlers2.FindNode(new Entry(key));
			if (node == null)
			{
				return;
			}
			EventHandler<TEventArgs> handler = node.Value.Handler as EventHandler<TEventArgs>;
			if (handler != null)
			{
				handler(sender, args);
			}
		}

		/// <summary>
		/// Raises the specified key.
		/// </summary>
		/// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
		/// <param name="key">The key.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="generateEventArgs">The generate event arguments.</param>
		public void Raise<TEventArgs>(string key, object sender, Func<TEventArgs> generateEventArgs) where TEventArgs : EventArgs
		{
			AvlTreeNode<Entry> node = _handlers2.FindNode(new Entry(key));
			if (node == null)
			{
				return;
			}
			EventHandler<TEventArgs> handler = node.Value.Handler as EventHandler<TEventArgs>;
			if (handler != null)
			{
				handler(sender, generateEventArgs());
			}
		}

		/// <summary>
		/// Removes the handler from the dictionary.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public void RemoveHandler(string key, Delegate value)
		{
			AvlTreeNode<Entry> node = _handlers2.FindNode(new Entry(key));
			if (node == null)
			{
				return;
			}
			node.Value.Handler = Delegate.Remove(node.Value.Handler, value);
		}

		/// <summary>
		/// Disposes the managed resources.
		/// </summary>
		protected override void DisposeManagedResources()
		{
			_handlers2.Clear();
		}

		private sealed class Entry : IComparable<Entry>
		{
			public Entry(string key, Delegate handler = null)
			{
				Key = key;
				Handler = handler;
			}

			//ToDo: WeakDelegate?
			public Delegate Handler { get; set; }
			public string Key { get; set; }

			public int CompareTo(Entry other)
			{
				return String.CompareOrdinal(Key, other.Key);
			}
		}
	}
}