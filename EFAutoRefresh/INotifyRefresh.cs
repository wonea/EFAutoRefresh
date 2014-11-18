// Copyright © 2012 by blueshell Software Engineering Harry von Borstel (http://www.blueshell.com)
// This work is licensed under COPL (see http://www.codeproject.com/info/cpol10.aspx)
// 
// 
using System.Collections.Specialized;

namespace EFAutoRefresh
{
	// see http://www.codeproject.com/KB/database/autorefresh_ef_ssb.aspx
	public interface INotifyRefresh : INotifyCollectionChanged
	{
		void OnRefresh();
	}

}
