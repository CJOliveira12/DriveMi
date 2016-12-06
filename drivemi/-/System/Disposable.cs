namespace System
{
	/// <summary>
	/// A simple <see cref="IDisposable"/> implementation.
	/// This implementation uses a single <see cref="EventHandler"/> for the Disposed event.
	/// Note: If you implement a finalizer, make sure that it calls Dispose(false).
	/// </summary>
	internal class Disposable : IDisposable
	{
		private bool _wasDisposed;

		/// <summary>
		///Event occurs when disposed.
		/// </summary>
		public event EventHandler Disposed = delegate { };

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
			if (_wasDisposed)
			{
				return;
			}
			EventHandler disposedHandler = Disposed;
			if (disposing)
			{
				DisposeManagedResources();
			}
			DisposeUnmanagedResources();
			_wasDisposed = true;
			if (disposedHandler != null)
			{
				disposedHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Disposes the managed resources.
		/// </summary>
		protected virtual void DisposeManagedResources() {}

		/// <summary>
		/// Disposes the unmanaged resources.
		/// </summary>
		protected virtual void DisposeUnmanagedResources() {}

		/// <summary>
		/// Throws the exception if disposed.
		/// </summary>
		/// <exception cref="ObjectDisposedException"><c>ObjectDisposedException</c>.</exception>
		protected void ThrowIfDisposed()
		{
			if (_wasDisposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
		}
	}
}