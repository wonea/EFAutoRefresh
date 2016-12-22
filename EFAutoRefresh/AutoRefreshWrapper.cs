// Copyright © 2012 by blueshell Software Engineering Harry von Borstel (http://www.blueshell.com)
// This work is licensed under COPL (see http://www.codeproject.com/info/cpol10.aspx)
// 
// 
using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace EFAutoRefresh
{
	// http://www.codeproject.com/KB/database/autorefresh_ef_ssb.aspx

	public class AutoRefreshWrapper<T> : IEnumerable<T>, INotifyRefresh
		where T : class
	{
		private readonly IEnumerable<T> _entitySet;
		public AutoRefreshWrapper(ObjectQuery<T> objectQuery, RefreshMode refreshMode)
		{
			_entitySet = objectQuery;
			objectQuery.Context.AutoRefresh(refreshMode, this);
		}

        //#if DBCONTEXT
        public AutoRefreshWrapper(DbSet<T> dbSet, DbContext dbContext, RefreshMode refreshMode)
        {
            _entitySet = dbSet;
            ((System.Data.Entity.Infrastructure.IObjectContextAdapter)dbContext).ObjectContext.AutoRefresh(refreshMode, this);
        }
        //#endif

		public void StopAutoRefresh()
		{
			ServiceBrokerUtility.StopAutoRefresh( this );
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _entitySet.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void OnRefresh()
		{
			try
			{
			    CollectionChanged?.Invoke(this,
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
