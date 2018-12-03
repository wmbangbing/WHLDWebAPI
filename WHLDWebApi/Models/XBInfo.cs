using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class XBInfo
    {
        //[Key]
        //[DisplayName("Id")]
        //public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("小班号")]
        public Int32 XBH { get; set; }

        [DisplayName("县/区")]
        public String DWMC { get; set; }

        [DisplayName("乡/街道")]
        public String XIANG { get; set; }

        [DisplayName("村/社区")]
        public String CUN { get; set; }

        [DisplayName("小班面积")]
        public System.Double? XBMJ { get; set; }

        [DisplayName("坡度")]
        public Int32? PD { get; set; }

        [DisplayName("海拔")]
        public System.Double? HB { get; set; }

        [DisplayName("经度")]
        public String JD { get; set; }

        [DisplayName("纬度")]
        public String WD { get; set; }

        [DisplayName("林地权属")]
        public String LDQS { get; set; }

        [DisplayName("林木权属")]
        public String LMQS { get; set; }

        [DisplayName("地类")]
        public String DL { get; set; }

        [DisplayName("林龄")]
        public Int32? LL { get; set; }

        [DisplayName("群落类型")]
        public String QLLX { get; set; }

        [DisplayName("造林时间")]
        public Int32? ZLSJ { get; set; }

        [DisplayName("树种组成")]
        public String SZZC { get; set; }

        [DisplayName("优势树种")]
        public String YSSZ { get; set; }

        [DisplayName("平均树高")]
        public System.Double? PJSG { get; set; }

        [DisplayName("平均胸径")]
        public System.Double? PJXJ { get; set; }

        [DisplayName("混交树种")]
        public String HJSZ { get; set; }

        [DisplayName("混交树种平均树高")]
        public System.Double? HJPJSG { get; set; }

        [DisplayName("混交树种平均胸径")]
        public System.Double? HJPJXJ { get; set; }

        [DisplayName("每亩蓄积")]
        public System.Double? MMXJ { get; set; }

        [DisplayName("龄组")]
        public String LZU { get; set; }

        [DisplayName("每亩株数")]
        public System.Double? MMZS { get; set; }

        [DisplayName("郁闭度")]
        public System.Double? YBD { get; set; }

        [DisplayName("森林健康等级")]
        public String SLJKDJ { get; set; }

        [DisplayName("群落结构")]
        public String QLJG { get; set; }

        [DisplayName("灌木种类")]
        public String GMZL { get; set; }

        [DisplayName("灌木盖度")]
        public Int32? GMGD { get; set; }

        [DisplayName("草本种类")]
        public String CBZL { get; set; }

        [DisplayName("草本盖度")]
        public Int32? CBGD { get; set; }

        [DisplayName("植被盖度")]
        public Int32? ZBGD { get; set; }

        [DisplayName("人为干扰")]
        public String RWGR { get; set; }

        [DisplayName("已有管护措施")]
        public String YYGHCS { get; set; }

        [DisplayName("管护形式/建议管护措施")]
        public String GHXS { get; set; }

        public System.Double? Area { get; set; }

        public Int32? Merge { get; set; }

        [DisplayName("计划")]
        public virtual ICollection<Plan> Plans { get; set; }

    }
}