﻿using System;
using System.IO;
using Common.Extensions;
using FileStorage.BL;
using FileStorage.BL.Tests;
using FileStorage.Core;
using FileStorage.Repository.Core;
using FileStorage.Repository.EF;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.EF;

namespace Pass.Container.BL.Tests
{
    public static class TestHelper
    {
        public const string CertificateFileApple = @"TestCertificates\Apple\pass.com.passlight.dev.test\pass.p12";
        public const string CertificateApplePassword = "Pass3";

        private const string AppleIconFileName = "icon.png";
        public static IPassContainerConfig PassContainerConfig { get { return new PassContainerConfig(); } }
        public static IPassContainerUnitOfWork PassContainerUnitOfWork { get { return new PassContainerUnitOfWork(new PassContainerConfig()); } }
        public static IFileStorageUnitOfWork FileStorageUnitOfWork { get { return new FileStorageUnitOfWork(new FileStorageConfig()); } }

        public static GeneralPassTemplate PreparePassTemplateSource(string testPassTemplateDir, string passTemplateFileName)
        {
            //Prepare pass template source
            GeneralPassTemplate generalTemplate = GetPassTemplateObject();
            generalTemplate.FieldDetails.BackFields.Add(new GeneralField
                                                                 {
                                                                     Key = "Key01",
                                                                     Label = "DynamicField",
                                                                     Value = "Test",
                                                                     Type = GeneralField.DataType.Text,
                                                                     IsDynamicValue = true
                                                                 });

            string path = Path.Combine(testPassTemplateDir, passTemplateFileName);
            if (File.Exists(path))
                File.Delete(path);

            generalTemplate.SaveToXml(path);
            File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestTemplates", "Template1", AppleIconFileName), Path.Combine(testPassTemplateDir, AppleIconFileName));
            return generalTemplate;
        }

        public static GeneralPassTemplate GetPassTemplateObject()
        {
            string generalPathTemplateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestTemplates", "Template1", "TestTemplate1.xml");
            var generalPassTemplate = generalPathTemplateFilePath.LoadFromXml<GeneralPassTemplate>();           
            return generalPassTemplate;
        }

        public static void ClearFileStorage(IFileStorageConfig fsConfig)
        {
            FsTestHelper.ClearFileStorage(fsConfig);
        }
    }
}
