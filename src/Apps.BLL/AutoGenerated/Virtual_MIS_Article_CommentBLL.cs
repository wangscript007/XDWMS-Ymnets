//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Apps.Models;
using Apps.Common;
using Unity.Attributes;
using System.Transactions;
using Apps.BLL.Core;
using Apps.Locale;
using LinqToExcel;
using System.IO;
using System.Text;
using Apps.IDAL.MIS;
using Apps.Models.MIS;
using Apps.IBLL.MIS;
namespace Apps.BLL.MIS
{
	public partial class MIS_Article_CommentBLL: Virtual_MIS_Article_CommentBLL,IMIS_Article_CommentBLL
	{
        

	}
	public class Virtual_MIS_Article_CommentBLL
	{
        [Dependency]
        public IMIS_Article_CommentRepository m_Rep { get; set; }

		public virtual List<MIS_Article_CommentModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<MIS_Article_Comment> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.ArticleId.Contains(queryStr)
								|| a.UserId.Contains(queryStr)
								|| a.TrueName.Contains(queryStr)
								|| a.IP.Contains(queryStr)
								|| a.BodyContent.Contains(queryStr)
								
								
								|| a.ReplyContent.Contains(queryStr)
								
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }

		public virtual List<MIS_Article_CommentModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<MIS_Article_CommentModel>();
		}
		
		public virtual List<MIS_Article_CommentModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<MIS_Article_CommentModel>();
		}

        public virtual List<MIS_Article_CommentModel> CreateModelList(ref IQueryable<MIS_Article_Comment> queryData)
        {

            List<MIS_Article_CommentModel> modelList = (from r in queryData
                                              select new MIS_Article_CommentModel
                                              {
													Id = r.Id,
													ArticleId = r.ArticleId,
													UserId = r.UserId,
													TrueName = r.TrueName,
													IP = r.IP,
													BodyContent = r.BodyContent,
													CreateTime = r.CreateTime,
													IsReply = r.IsReply,
													ReplyContent = r.ReplyContent,
													ReplyTime = r.ReplyTime,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, MIS_Article_CommentModel model)
        {
            try
            {
                MIS_Article_Comment entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new MIS_Article_Comment();
               				entity.Id = model.Id;
				entity.ArticleId = model.ArticleId;
				entity.UserId = model.UserId;
				entity.TrueName = model.TrueName;
				entity.IP = model.IP;
				entity.BodyContent = model.BodyContent;
				entity.CreateTime = model.CreateTime;
				entity.IsReply = model.IsReply;
				entity.ReplyContent = model.ReplyContent;
				entity.ReplyTime = model.ReplyTime;
  

                if (m_Rep.Create(entity))
                {
                    return true;
                }
                else
                {
                    errors.Add(Resource.InsertFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }



         public virtual bool Delete(ref ValidationErrors errors, object id)
        {
            try
            {
                if (m_Rep.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public virtual bool Delete(ref ValidationErrors errors, object[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        if (m_Rep.Delete(deleteCollection) == deleteCollection.Length)
                        {
                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

		
       

        public virtual bool Edit(ref ValidationErrors errors, MIS_Article_CommentModel model)
        {
            try
            {
                MIS_Article_Comment entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.ArticleId = model.ArticleId;
				entity.UserId = model.UserId;
				entity.TrueName = model.TrueName;
				entity.IP = model.IP;
				entity.BodyContent = model.BodyContent;
				entity.CreateTime = model.CreateTime;
				entity.IsReply = model.IsReply;
				entity.ReplyContent = model.ReplyContent;
				entity.ReplyTime = model.ReplyTime;
 


                if (m_Rep.Edit(entity))
                {
                    return true;
                }
                else
                {
                    errors.Add(Resource.NoDataChange);
                    return false;
                }

            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

      

        public virtual MIS_Article_CommentModel GetById(object id)
        {
            if (IsExists(id))
            {
                MIS_Article_Comment entity = m_Rep.GetById(id);
                MIS_Article_CommentModel model = new MIS_Article_CommentModel();
                              				model.Id = entity.Id;
				model.ArticleId = entity.ArticleId;
				model.UserId = entity.UserId;
				model.TrueName = entity.TrueName;
				model.IP = entity.IP;
				model.BodyContent = entity.BodyContent;
				model.CreateTime = entity.CreateTime;
				model.IsReply = entity.IsReply;
				model.ReplyContent = entity.ReplyContent;
				model.ReplyTime = entity.ReplyTime;
 
                return model;
            }
            else
            {
                return null;
            }
        }


		 /// <summary>
        /// 校验Excel数据,这个方法一般用于重写校验逻辑
        /// </summary>
        public virtual bool CheckImportData(string fileName, List<MIS_Article_CommentModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<MIS_Article_CommentModel>(x => x.ArticleId, "ArticleId");
				 excelFile.AddMapping<MIS_Article_CommentModel>(x => x.UserId, "UserId");
				 excelFile.AddMapping<MIS_Article_CommentModel>(x => x.TrueName, "TrueName");
				 excelFile.AddMapping<MIS_Article_CommentModel>(x => x.IP, "IP");
				 excelFile.AddMapping<MIS_Article_CommentModel>(x => x.BodyContent, "BodyContent");
				 excelFile.AddMapping<MIS_Article_CommentModel>(x => x.CreateTime, "CreateTime");
				 excelFile.AddMapping<MIS_Article_CommentModel>(x => x.IsReply, "IsReply");
				 excelFile.AddMapping<MIS_Article_CommentModel>(x => x.ReplyContent, "ReplyContent");
				 excelFile.AddMapping<MIS_Article_CommentModel>(x => x.ReplyTime, "ReplyTime");
 
            //SheetName
            var excelContent = excelFile.Worksheet<MIS_Article_CommentModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new MIS_Article_CommentModel();
						 				  entity.Id = row.Id;
				  entity.ArticleId = row.ArticleId;
				  entity.UserId = row.UserId;
				  entity.TrueName = row.TrueName;
				  entity.IP = row.IP;
				  entity.BodyContent = row.BodyContent;
				  entity.CreateTime = row.CreateTime;
				  entity.IsReply = row.IsReply;
				  entity.ReplyContent = row.ReplyContent;
				  entity.ReplyTime = row.ReplyTime;
 
                //=============================================================================
                if (errorMessage.Length > 0)
                {
                    errors.Add(string.Format(
                        "第 {0} 列发现错误：{1}{2}",
                        rowIndex,
                        errorMessage,
                        "<br/>"));
                }
                list.Add(entity);
                rowIndex += 1;
            }
            if (errors.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public virtual void SaveImportData(IEnumerable<MIS_Article_CommentModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        MIS_Article_Comment entity = new MIS_Article_Comment();
                       						entity.Id = ResultHelper.NewId;
						entity.ArticleId = model.ArticleId;
						entity.UserId = model.UserId;
						entity.TrueName = model.TrueName;
						entity.IP = model.IP;
						entity.BodyContent = model.BodyContent;
						entity.CreateTime = ResultHelper.NowTime;
						entity.IsReply = model.IsReply;
						entity.ReplyContent = model.ReplyContent;
						entity.ReplyTime = model.ReplyTime;
 
                        db.MIS_Article_Comment.Add(entity);
                    }
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
		public virtual bool Check(ref ValidationErrors errors, object id,int flag)
        {
			return true;
		}

        public virtual bool IsExists(object id)
        {
            return m_Rep.IsExist(id);
        }
		
		public void Dispose()
        { 
            
        }

	}
}
