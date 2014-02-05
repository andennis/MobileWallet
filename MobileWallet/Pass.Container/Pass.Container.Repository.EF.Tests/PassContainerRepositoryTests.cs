﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Pass.Container.Core.Entities.Enums;
using PassEntities = Pass.Container.Core.Entities;

namespace Pass.Container.Repository.EF.Tests
{
    [TestFixture]
    public class PassContainerRepositoryTests
    {
        [Test]
        public void PassCrudOperationsTest()
        {
            using (var unitOfWork = new PassContainerUnitOfWork(TestHelper.DbConfig))
            {
                var repPassTemplate = unitOfWork.GetRepository<PassEntities.PassTemplate>();
                var repTemplateNative = unitOfWork.GetRepository<PassEntities.PassTemplateNative>();
                var pt1 = new PassEntities.PassTemplate() { Name = "PT1", PackageId = 1 };

                var ptApple1 = new PassEntities.PassTemplateApple() {PassTypeId = "PTID1", Template = pt1};
                repPassTemplate.Insert(pt1);
                repTemplateNative.Insert(ptApple1);
                unitOfWork.Save();
                Assert.Greater(pt1.PassTemplateId, 0);
                Assert.Greater(ptApple1.PassTemplateNativeId, 0);

                var pass1 = new PassEntities.Pass() {AuthToken = "123", SerialNumber = "123", Status = PassStatus.Active, Template = pt1};
                var repPass = unitOfWork.GetRepository<PassEntities.Pass>();
                repPass.Insert(pass1);

                var passField1 = new PassEntities.PassField() {Name = "PF1", Template = pt1};
                var repPassField = unitOfWork.GetRepository<PassEntities.PassField>();
                repPassField.Insert(passField1);

                var passFieldValue1 = new PassEntities.PassFieldValue() {Pass = pass1, PassField = passField1, Value = "000"};
                var repPassFieldValue = unitOfWork.GetRepository<PassEntities.PassFieldValue>();
                repPassFieldValue.Insert(passFieldValue1);

                unitOfWork.Save();
                Assert.Greater(pass1.PassId, 0);
                Assert.Greater(passField1.PassFieldId, 0);
                Assert.Greater(passFieldValue1.PassFieldValueId, 0);

                PassEntities.PassTemplate pt2 = repPassTemplate.Query()
                    .Include(x => x.NativeTemplates)
                    .Include(x => x.Passes)
                    .Filter(x => x.PassTemplateId == pt1.PassTemplateId)
                    .Get()
                    .FirstOrDefault();
                
                Assert.NotNull(pt2);
                Assert.AreEqual(pt1.Name, pt2.Name);
                Assert.NotNull(pt2.NativeTemplates);
                pt2.NativeTemplates.Should().HaveCount(1);
                pt2.NativeTemplates.OfType<PassEntities.PassTemplateApple>().Should().HaveCount(1);
                Assert.NotNull(pt2.Passes);
                pt2.NativeTemplates.Should().HaveCount(1);

                pt1.Name = pt1.Name + "_New";
                repPassTemplate.Update(pt1);
                unitOfWork.Save();

                pt2 = repPassTemplate.Find(pt1.PassTemplateId);
                Assert.NotNull(pt2);
                Assert.AreEqual(pt1.Name, pt2.Name);

                var pass2 = repPass.Find(pass1.PassId);
                Assert.NotNull(pass2);
                Assert.AreEqual(pass1.SerialNumber, pass2.SerialNumber);
                pass1.SerialNumber = pass1.SerialNumber + "New";
                repPass.Update(pass1);
                unitOfWork.Save();
                pass2 = repPass.Find(pass1.PassId);
                Assert.AreEqual(pass1.SerialNumber, pass2.SerialNumber);

                var passField2 = repPassField.Find(passField1.PassFieldId);
                Assert.NotNull(passField2);
                Assert.AreEqual(passField1.Name, passField2.Name);
                passField1.Name += "New";
                repPassField.Update(passField1);
                unitOfWork.Save();
                passField2 = repPassField.Query().Filter(x => x.PassFieldId == passField1.PassFieldId).Get().FirstOrDefault();
                Assert.NotNull(passField2);
                Assert.AreEqual(passField1.Name, passField2.Name);

                var passFieldValue2 = repPassFieldValue.Find(passFieldValue1.PassFieldValueId);
                Assert.NotNull(passFieldValue2);
                Assert.AreEqual(passFieldValue1.Value, passFieldValue2.Value);
                passFieldValue1.Value += "New";
                repPassFieldValue.Update(passFieldValue1);
                unitOfWork.Save();
                passFieldValue2 = repPassFieldValue.Query().Filter(x => x.PassFieldValueId == passFieldValue1.PassFieldValueId).Get().FirstOrDefault();
                Assert.NotNull(passFieldValue2);
                Assert.AreEqual(passFieldValue1.Value, passFieldValue2.Value);

                repPassFieldValue.Delete(passFieldValue1);
                repPass.Delete(pass1);
                unitOfWork.Save();

                repPassField.Delete(passField1);
                repTemplateNative.Delete(ptApple1);
                repPassTemplate.Delete(pt1);
                unitOfWork.Save();

                Assert.IsNull(repPass.Query().Filter(x => x.PassId == pass1.PassId).Get().FirstOrDefault());
                Assert.IsNull(repPassTemplate.Query().Filter(x => x.PassTemplateId == pt1.PassTemplateId).Get().FirstOrDefault());
            }
        }

        [Test]
        public void ClientDeviceCrudOperationsTest()
        {
            using (var unitOfWork = new PassContainerUnitOfWork(TestHelper.DbConfig))
            {
                var repClientDev = unitOfWork.GetRepository<PassEntities.ClientDevice>();
                var cd1 = new PassEntities.ClientDeviceApple() {DeviceId = "Dev1", PushToken = "555"};
                repClientDev.Insert(cd1);
                unitOfWork.Save();
                Assert.Greater(cd1.ClientDeviceId, 0);

                var cd2 = (PassEntities.ClientDeviceApple)repClientDev.Query()
                    .Filter(x => x.ClientDeviceId == cd1.ClientDeviceId)
                    .Get()
                    .FirstOrDefault();
                Assert.NotNull(cd2);
                Assert.AreEqual(cd1.PushToken, cd2.PushToken);

                cd1.DeviceId += "New";
                repClientDev.Update(cd1);
                unitOfWork.Save();

                cd2 = (PassEntities.ClientDeviceApple)repClientDev.Query()
                    .Filter(x => x.ClientDeviceId == cd1.ClientDeviceId)
                    .Get()
                    .FirstOrDefault();
                Assert.NotNull(cd2);
                Assert.AreEqual(cd1.DeviceId, cd2.DeviceId);

                var repPassTemplate = unitOfWork.GetRepository<PassEntities.PassTemplate>();
                var pt1 = new PassEntities.PassTemplate() { Name = "PT1", PackageId = 1 };
                repPassTemplate.Insert(pt1);
                unitOfWork.Save();

                var pass1 = new PassEntities.Pass() { AuthToken = "123", SerialNumber = "123", Status = PassStatus.Active, Template = pt1 };
                var repPass = unitOfWork.GetRepository<PassEntities.Pass>();
                repPass.Insert(pass1);
                unitOfWork.Save();
                
                var registration1 = new PassEntities.Registration() {ClientDeviceId = cd1.ClientDeviceId, PassId = pass1.PassId, Status = RegistrationStatus.Active};
                var repReg = unitOfWork.GetRepository<PassEntities.Registration>();
                repReg.Insert(registration1);
                unitOfWork.Save();

                PassEntities.Registration registration2 = repReg.Query()
                    .Filter(x => x.ClientDeviceId == registration1.ClientDeviceId)
                    .Filter(x => x.PassId == pass1.PassId)
                    .Include(x => x.Pass)
                    .Include(x => x.ClientDevice)
                    .Get().FirstOrDefault();
                Assert.NotNull(registration2);
                Assert.NotNull(registration2.ClientDevice);
                Assert.NotNull(registration2.Pass);
                Assert.AreEqual(registration1.Status, registration2.Status);

            }
        }
    }
}
