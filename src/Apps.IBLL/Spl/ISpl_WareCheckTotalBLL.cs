//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Apps.Common;
using System.Collections.Generic;
using Apps.Models.Spl;
using Apps.Models;

namespace Apps.IBLL.Spl
{
	public partial interface ISpl_WareCheckTotalBLL
	{
        /// <summary>
        /// 获取现有库存信息
        /// </summary>
        /// <param name="warehouseid"></param>
        /// <param name="waredetailsid"></param>
        /// <returns></returns>
        Spl_WareStockPileModel GetQuantity(string warehouseid, string waredetailsid);
        bool Check(ref ValidationErrors errors, string id, int checkFlag, string checker);

        List<Spl_WareCheckTotalModel> GetListByParentId(ref GridPager pager, string queryStr, object parentId, string sysUserId);
        List<Spl_WareCheckTotalModel> GetList(ref GridPager pager, string queryStr, string sysUserId);
    }
}
