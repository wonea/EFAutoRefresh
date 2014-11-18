// Copyright © 2012 by blueshell Software Engineering Harry von Borstel (http://www.blueshell.com)
// This work is licensed under COPL (see http://www.codeproject.com/info/cpol10.aspx)
// 
// 
using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Collections;
using System.Collections.Specialized;
using System.Data.Entity;

namespace EFAutoRefresh
{
	// http://www.codeproject.com/KB/database/autorefresh_ef_ssb.aspx

	public class AutoRefreshWrapper<T> : IEnumerable<T>, INotifyRefresh
		where T : class
	{
		private IEnumerable<T> entitySet;
		public AutoRefreshWrapper(ObjectQuery<T> objectQuery, RefreshMode refreshMode)
		{
			this.entitySet = objectQuery;
			objectQuery.Context.AutoRefresh(refreshMode, this);
		}

#if DBCONTEXT
        public AutoRefreshWrapper(DbSet<T> dbSet, DbContext dbContext, RefreshMode refreshMode)
        {
            this.entitySet = dbSet;
            ((System.Data.Entity.Infrastructure.IObjectContextAdapter)dbContext).ObjectContext.AutoRefresh(refreshMode, this);
        }
#endif

		public void StopAutoRefresh()
		{
			ServiceBrokerUtility.StopAutoRefresh( this );
		}

		public IEnumerator<T> GetEnumerator()
		{
			return entitySet.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public void OnRefresh()
		{
			try
			{
				if (this.CollectionChanged != null)
					CollectionChanged(this,
					  new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.Print("Error in OnRefresh: {0}", ex.Message);
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
