﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntDeploy.Util;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {


            //var buildResult = CommandHelper.RunMsbuild("E:\\WorkSpace\\github\\AntDeploy\\AntDeployAgentWindowsService\\AntDeployAgentWindowsService.csproj",
            //    Console.WriteLine,Console.WriteLine);

            var stream = ZipHelper.DoCreateFromDirectory2(
                @"H:\Csharp\yuzd\Lito\Lito\Lito.APP\bin\Release\netcoreapp2.1\publish",
                CompressionLevel.Optimal, true, new List<string>
                {
                    "appsettings.*",
                    "web.config",
                    "QRCoder.dll"
                });




            SSHClient sshClient = new SSHClient("192.168.0.7:22","root","kawayiyi@1",Console.WriteLine);
            sshClient.Connect();
            sshClient.PublishZip(stream,"/publisher","publish.zip");



            var bytes= ZipHelper.DoCreateFromDirectory(
                @"H:\Csharp\yuzd\Lito\Lito\Lito.APP\bin\Release\netcoreapp2.1\publish",
                CompressionLevel.Optimal, true,new List<string>
                {
                    "appsettings.*",
                    "web.config",
                    "QRCoder.dll"
                });


            
            var filePath = Path.Combine(@"H:\Csharp\yuzd\AntDeploy\AntDeploy\WindowsFormsAppTest\Test\MyProject\bin\Release\netstandard2.0\unzip", "aa.zip");
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(bytes,0, bytes.Length);
            }

            //解压
            try
            {
                ZipFile.ExtractToDirectory(filePath, @"H:\Csharp\yuzd\AntDeploy\AntDeploy\WindowsFormsAppTest\Test\MyProject\bin\Release\netstandard2.0\unzip");
            }
            catch (Exception ex)
            {

            }


            return;

            CommandHelper.RunDotnetExternalExe(@"H:\Csharp\yuzd\AntDeploy\AntDeploy\WindowsFormsAppTest\Test\MyProject","dotnet",
                "publish -c Release",
                Console.WriteLine,
                Console.WriteLine);
            Console.ReadKey();
        }
    }
}
