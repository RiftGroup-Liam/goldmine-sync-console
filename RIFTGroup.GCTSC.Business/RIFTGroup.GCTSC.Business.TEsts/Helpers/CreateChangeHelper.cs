using RIFTGroup.GCTSC.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Business.Tests.Helpers
{
    public class CreateChangeHelper
    {
        string _testAccountno;
        public CreateChangeHelper(string testAccountno)
        {
            _testAccountno = testAccountno;
        }
        public void CreateContSuppChange()
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                int result = context.TESTS_CreateContsuppChange(_testAccountno);
                if (result <= 0)
                {
                    throw new Exception("Can't create contsupp change");
                }
            }
        }
        public void CreateContact2Change()
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                int result = context.TESTS_CreateContact2Change(_testAccountno);
                if (result <= 0)
                {
                    throw new Exception("Can't create contact2 change");
                }
            }
        }
        public void CreateContact1Change()
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                int result = context.TESTS_CreateContact1Change(_testAccountno);
                if (result <= 0)
                {
                    throw new Exception("Can't create contact1 change");
                }
            }
        }
    }
}
