using System.ComponentModel;

namespace System
{
	/// <summary>
	/// A base <see cref="IDisposable"/> implementation.
	/// This implementation uses a <see cref="EventHandlerDictionary"/> for storing the Disposed event.
	/// Use <see cref="Disposable"/> if you do not need a base <see cref="EventHandlerDictionary"/>.
	/// 
	/// Note: If you implement a finalizer, make sure that it calls Dispose(false).
	/// </summary>
	internal abstract class DisposableWithEvents : IDisposable
	{
		private bool _disposed;
		private EventHandlerDictionary _events;

		/// <summary>
		/// Gets a value indicating whether [event store created].
		/// </summary>
		/// <value><c>true</c> if [event store created]; otherwise, <c>false</c>.</value>
		protected bool EventStoreCreated
		{
			get { return (_events != null); }
		}

		/// <summary>
		/// Gets the event handlers dictionary.
		/// </summary>
		/// <value>The events.</value>
		protected EventHandlerDictionary Events
		{
			get
			{
				EnsureEventHandlerDictionary();
				return _events;
			}
		}

		/// <summary>
		/// Event occurs when disposed.
		/// </summary>
		public event EventHandler Disposed
		{
			add
			{
				EnsureEventHandlerDictionary();
				_events.AddHandler(EventKey.Disposed, value);
			}
			remove
			{
				EnsureEventHandlerDictionary();
				_events.RemoveHandler(EventKey.Disposed, value);
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}
			EventHandler disposedHandler = null;
			if (EventStoreCreated)
			{
				disposedHandler = _events[EventKey.Disposed] as EventHandler;
			}
			if (disposing)
			{
				DisposeManagedResources();
				if (EventStoreCreated)
				{
					_events.Dispose();
				}
			}
			DisposeUnmanagedResources();
			_disposed = true;
			if (disposedHandler != null)
			{
				disposedHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Disposes the managed resources.
		/// </summary>
		protected virtual void DisposeManagedResources()
		{
		}

		/// <summary>
		/// Disposes the unmanaged resources.
		/// </summary>
		protected virtual void DisposeUnmanagedResources()
		{
		}

		/// <summary>
		/// Determines whether [has handler for] [the specified event key].
		/// </summary>
		/// <param name="eventKey">The event key.</param>
		/// <returns>
		/// 	<c>true</c> if [has handler for] [the specified event key]; otherwise, <c>false</c>.
		/// </returns>
		protected bool IsHandlerRegisteredFor(string eventKey)
		{
			return (_events != null) && (_events[eventKey] != null);
		}

		/// <summary>
		/// Throws the exception if disposed.
		/// </summary>
		/// <exception cref="ObjectDisposedException"><c>ObjectDisposedException</c>.</exception>
		protected void ThrowExceptionIfDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
		}

		private void EnsureEventHandlerDictionary()
		{
			if (_events == null)
			{
				_events = new EventHandlerDictionary();
			}
		}

		private static class EventKey
		{
			public const string Disposed = "Disposed";
		}
	}
}