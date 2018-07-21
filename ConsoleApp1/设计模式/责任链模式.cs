using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.设计模式
{
    //实体类
    public class ApplyContext
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Msg { get; set; }

        public int Time { get; set; }

        public bool State { get; set; }
    }


    public abstract class AbstractAuditor
    {
        public string Name { get; set; }

        //请假
        public abstract void Audit(ApplyContext context);

        protected AbstractAuditor _nextAuditor = null;

        public void SetNext(AbstractAuditor context)
        {
            this._nextAuditor = context;
        }
    }

    //员工
    public class Staff : AbstractAuditor
    {
        public override void Audit(ApplyContext context)
        {
            base._nextAuditor.Audit(context); 
        }
    }


    //经理
    public class PM : AbstractAuditor
    {
        public override void Audit(ApplyContext context)
        {
            if (context.Time < 12)
            {
                context.State = true;
            }
            else
            {
                if (base._nextAuditor != null)
                {
                    base._nextAuditor.Audit(context);
                }
            }
        }
    }

    //CEO
    public class CEO : AbstractAuditor
    {
        public override void Audit(ApplyContext context)
        {
            if (context.Time < 24)
            {
                context.State = true;
            }
            else
            {
                if (base._nextAuditor != null)
                {
                    base._nextAuditor.Audit(context);
                }
            }
        }
    }




    class 责任链模式
    {
        //static void Main(string[] args)
        //{
        //    AbstractAuditor staff = new Staff() { Name = "pmName" };
        //    AbstractAuditor pm = new PM() { Name = "pmName" };
        //    AbstractAuditor ceo = new CEO { Name = "Ceo" };

        //    staff.SetNext(pm);
        //    pm.SetNext(ceo);
        //    //ceo.SetNext();

        //    staff.Audit(new ApplyContext() { Name="小明",Msg="我要请假"});

        //}
    }
}
