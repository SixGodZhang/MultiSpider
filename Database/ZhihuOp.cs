using LogManager;
using Repos;
using Spider.MultiDbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Spider.Database
{
    class ZhihuOp : IDatabaseOp
    {
        private static ZhihuOp _instance;
        public static ZhihuOp Instance
        {
            get {
                if (_instance == null) _instance = new ZhihuOp();
                return _instance;
            }
        }

        /// <summary>
        /// 查询满足时间条件的热点数据
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="context"></param>
        /// <param name="conditionDate">时间精确到年月日时</param>
        /// <returns></returns>
        /// ///////////////////////////////
        /// ZhihuOp.Instance.GetRangeHotData(new DateTime(2018, 11, 14, 16, 0, 0));
        /// //////////////////////////////
        public List<HotRepo> GetRangeHotData(DateTime conditionDate)
        {
            ATLog.Info("从数据库查询符合条件的热点数据");
            List<HotRepo> finals = new List<HotRepo>();
            using (ZhiHuContext context = new ZhiHuContext())
            {
                var results = context.Set<HotRepo>().AsQueryable().AsNoTracking();
                finals = results.Where(c => conditionDate.Year == c.Date.Year
                                        && conditionDate.Month == c.Date.Month
                                        && conditionDate.Day == c.Date.Day
                                        && conditionDate.Hour == c.Date.Hour).ToList();
            }
            return finals;
        }

        /// <summary>
        /// 保存单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void SaveData<T>(T data) where T : IRepo
        {
            using (ZhiHuContext context = new ZhiHuContext())
            {
                if (data.GetType() == typeof(HotRepo))
                    context.HotEntities.Add(data as HotRepo);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        public void SaveRangeData<T>(IEnumerable<T> datas) where T : IRepo
        {
            ATLog.Info("存储知乎的热点数据到数据库");
            using (ZhiHuContext context = new ZhiHuContext())
            {
                if (typeof(T) == typeof(HotRepo))
                    context.HotEntities.AddRange(datas as IEnumerable<HotRepo>);
                context.SaveChanges();
            }
        }
    }
}
