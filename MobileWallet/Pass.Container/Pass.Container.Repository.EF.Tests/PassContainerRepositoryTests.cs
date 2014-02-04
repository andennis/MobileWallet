using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using PassEntities = Pass.Container.Core.Entities;

namespace Pass.Container.Repository.EF.Tests
{
    [TestFixture]
    public class PassContainerRepositoryTests
    {
        [Test]
        public void PassCrudOperationsTest()
        {
            PassEntities.PassTemplate pt1 = null;
            using (var unitOfWork = new PassContainerUnitOfWork(TestHelper.DbConfig))
            {
                var repPassTemplate = unitOfWork.GetRepository<PassEntities.PassTemplate>();
                var repAppleTemplate = unitOfWork.GetRepository<PassEntities.PassTemplateApple>();
                pt1 = new PassEntities.PassTemplate()
                          {
                              Name = "PT1", 
                              PackageId = 1,
                              /*
                              NativeTemplates = new Collection<PassEntities.PassTemplateNative>()
                                          {
                                              new PassEntities.PassTemplateApple(){PassTypeId = "PTID1"}
                                          },
                              Passes =  new Collection<PassEntities.Pass>()
                                            {
                                                new PassEntities.Pass(){AuthToken = "123", SerialNumber = "123", Status = PassStatus.Active }
                                            },
                              
                              PassFields = new Collection<PassEntities.PassField>(),
                              */
                          };

                var pat1 = new PassEntities.PassTemplateApple() {PassTypeId = "PTID1", Template = pt1};
                repPassTemplate.Insert(pt1);
                repAppleTemplate.Insert(pat1);
                unitOfWork.Save();
                Assert.Greater(pt1.PassTemplateId, 0);
            //}

            //using (unitOfWork = new PassContainerUnitOfWork(dbSession))
            //{
                var rep = unitOfWork.GetRepository<PassEntities.PassTemplate>();
                PassEntities.PassTemplate copyPt1 = rep.Find(pt1.PassTemplateId);
                /*
                PassEntities.PassTemplate copyPt1 = rep.Query()
                    .Include(x => x.NativeTemplates)
                    .Filter(x => x.PassTemplateId == pt1.PassTemplateId)
                    .Get()
                    .FirstOrDefault();
                */
                Assert.NotNull(copyPt1);
                Assert.AreEqual(pt1.Name, copyPt1.Name);
                /*
                Assert.NotNull(copyPt1.NativeTemplates);
                copyPt1.NativeTemplates.Should().HaveCount(1);
                copyPt1.NativeTemplates.OfType<PassEntities.PassTemplateApple>().Should().HaveCount(1);
                */

                pt1.Name = pt1.Name + "_New";
                rep.Update(pt1);
                unitOfWork.Save();

                copyPt1 = rep.Find(pt1.PassTemplateId);
                Assert.NotNull(copyPt1);
                Assert.AreEqual(pt1.Name, copyPt1.Name);
            }
        }
    }
}
