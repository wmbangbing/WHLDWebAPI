using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<DbEntities>
    //public class SampleData : DropCreateDatabaseAlways<DbEntities>
    {
        protected override void Seed(DbEntities context)
        {
            new List<Ghcs>
            {
                new Ghcs{GId=1,Measure="常规管护",Desc="林带看护、病虫害预测预报"},
                new Ghcs{GId=2,Measure="林下清理与树盘整理",Desc="林下植物以灌木为主，灌木丛生、盖度较大，影响幼树生长时"},
                new Ghcs{GId=3,Measure="病虫害防治",Desc="林分病虫害等级为重度及中等时"},
                new Ghcs{GId=4,Measure="化学除草",Desc="林下植物以草本为主，草本盖度较大、杂草丛生，影响幼树生长时"},
                new Ghcs{GId=5,Measure="整枝修剪",Desc="在一些关键区域，林分密度适宜，但阳性树种枝叶密集、自然整枝较严重、树形较差时"},
                new Ghcs{GId=6,Measure="追肥施肥",Desc="立地条件差、幼龄林生长势较差时"},
                new Ghcs{GId=7,Measure="营养调节",Desc="林分优势种密度大、郁闭度大，应采取抚育性间伐或移栽"},
                new Ghcs{GId=8,Measure="混交添色",Desc="在一些关键区域，林分密度较小、郁闭度不大、树种单一时，应补植一些色叶树种进行混交添色"},
                new Ghcs{GId=9,Measure="近自然改造",Desc="小班树种单一、病虫害严重、生长势较差的中龄林、近熟林林分，应以乡土树种补植为主，使之形成异龄、复层、混交的近自然群落"},
                new Ghcs{GId=10,Measure="补植补造",Desc="在一些非关键区域，林分密度较小、郁闭度不大，小班局部区域形成林窗的中幼龄林，应进行补植补造"},
                new Ghcs{GId=11,Measure="复绿造林",Desc="林带范围内撂荒地或临时工地、或因工程建设林分被损坏的地段及其他原因形成的裸地，应及时开展复绿造林"},
                new Ghcs{GId=12,Measure="更新造林",Desc="林分密度大、郁闭度大，进入近熟林阶段时应建议近期进行更新造林，林分进入成熟林或过熟林阶段时应立即进行更新造林"},
                new Ghcs{GId=13,Measure="生态围网",Desc="村庄附近的中幼龄小班，受到火灾、放牧、偷盗、间种、间养、墓地等人为干扰，应采用绿篱生态围网防护措施"},
                new Ghcs{GId=14,Measure="灌溉设施建设",Desc="局部高地不耐旱树种栽植的小班应增加灌溉设施或洒水车租赁灌溉"},
                new Ghcs{GId=15,Measure="排涝设施建设",Desc="低湿地段不耐涝树种栽植的小班应进行排水沟建设"},
                new Ghcs{GId=16,Measure="防火设施建设",Desc="在村庄附近人为干扰较大的小班应建设防火设施"},
                new Ghcs{GId=17,Measure="监测点建设",Desc="村庄附近人为干扰较大的小班应建设监测"},
            }.ForEach(a => context.Ghcss.Add(a));

            new List<Task>
            {
                new Task{TId=1,Title="任务一",DateTime="2018/5/5",Person="张三"},
                new Task{TId=2,Title="任务二",DateTime="2018/6/5",Person="李四"},
                //new Task{TId=3,Title="任务三",DateTime="2018/7/5",Person="王五"},
            }.ForEach(a => context.Tasks.Add(a));

            new List<XBInfo>
            {
                new XBInfo{XBH=1171028,XBMJ=4.469952},
                new XBInfo{XBH=1171029,XBMJ=2.644659},
                new XBInfo{XBH=1171030,XBMJ=8.383033},
                new XBInfo{XBH=1171031,XBMJ=13.643084},
                new XBInfo{XBH=1171032,XBMJ=3.217957},
                //new XBInfo{XBH=1171033,Area=24.487969},
            }.ForEach(a => context.XBInfos.Add(a));

            new List<PlanGhcsRel>
            {
                //new PlanGhcsRel{PId=1,GId=1},
                //new PlanGhcsRel{PId=1,GId=7},
                //new PlanGhcsRel{PId=1,GId=10},
                new PlanGhcsRel{PId=2,GId=1},
                new PlanGhcsRel{PId=2,GId=5},
                new PlanGhcsRel{PId=2,GId=6},
                new PlanGhcsRel{PId=3,GId=1},
                new PlanGhcsRel{PId=3,GId=6},
                new PlanGhcsRel{PId=3,GId=8},
                //new PlanGhcsRel{PId=4,GId=1},
                //new PlanGhcsRel{PId=4,GId=7},
                //new PlanGhcsRel{PId=4,GId=6},
                new PlanGhcsRel{PId=5,GId=1},
                new PlanGhcsRel{PId=5,GId=2},
                new PlanGhcsRel{PId=5,GId=3},
                new PlanGhcsRel{PId=6,GId=1},
                new PlanGhcsRel{PId=6,GId=5},
                new PlanGhcsRel{PId=6,GId=7},
            }.ForEach(a => context.PlanGhcsRels.Add(a));

            new List<Plan>
            {
                //new Plan{PId=1,TId=3,XBH=1171028},
                new Plan{PId=2,TId=2,XBH=1171029},
                new Plan{PId=3,TId=1,XBH=1171030},
                //new Plan{PId=4,TId=3,XBH=1171031},
                new Plan{PId=5,TId=2,XBH=1171032},
                new Plan{PId=6,TId=1,XBH=1171028},
            }.ForEach(a => context.Plans.Add(a));

            new List<User>
            {
                new User{Id=1,UserName="admin",PassWord="123456",Remark="All"},
                new User{Id=1,UserName="Hongshan",PassWord="123456",Remark="HS"},
                new User{Id=1,UserName="Jiangxia",PassWord="123456",Remark="JX"},
            }.ForEach(a => context.Users.Add(a));
            context.SaveChanges();
        }
    }
}