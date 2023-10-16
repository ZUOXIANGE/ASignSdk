﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SHA1withRSADemo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        //APPID
        public static string APPID = "223299143";
        //public static string APPID = "2019062133";
        //私钥
        public static string privateKey = "MIIBUwIBADANBgkqhkiG9w0BAQEFAASCAT0wggE5AgEAAkEAmt2jO6Z5uwRFM+bdRBXoHcjpdWpUDvwI05py3kZvfcg7ahUJkz4cQvCUaLVMFvH4LyefiA5ER2q7S87653yYMQIDAQABAkBzD4Eb7JA89utDqJ902qHen0t1RU6242LbdMErjEGBvXjzOvLpigUaB8gSR+o1r+damPQJdZWbR4+8EBvK1FORAiEA9EgQVjzmhFXEjsCLo/gWlbnjQhyByV7ZEotosBlm/bsCIQCiS32HTC4AwvBT1gzyr11zWmjxizYtYwV6NryvUE1tAwIgQjh+5UHhI6K0hBZCRJLuXGxl5PghXtttcQ+Fs6dPOh0CIGAq/10WpQPKf4IOCmobw/JAloLajOXkETDUEoaHvPllAiAsYPUVC4fmNW53YeDJiCcQ9yl/WbH6mktfzVl011KBcA==";

        //测试请求路径，生产环境时改成正式的地址
        public static string requestUrl = "http://prev.acsign.cn";

        static void Main(string[] args)
        {
            #region 注意事项
            
            //1、表单提交方式：form-data
            //2、请求头部参数
            //    参数1：sign（签名值，具体算法参考一下的前面算法）
            //    参数2：timestamp（时间戳，13位）
            //3、请求体参数：
            //    参数1：appId（appId值，每个接入者唯一一个）
            //    参数2：timestamp（时间戳，13位，与上述一致） 特别注意，时间要加10分钟。不要问为什么，我也不知道
            //    参数3：bizData（json字符串，举个例子，比方说要传合同编号如：{"contractNo":"0001"}）
            //4、签名算法：
            //    4.1、将上述3所属的bizData（json字符串），按照阿拉伯字母排序(如：{"ba":1,"ac":2}--->{"ac":2,"ba":1})，
            //    4.2、将4.1排序后的字符串，将【bizData+md5（bizData）+ appId + timestatmp】拼接后利用RSA非对称加密算法（SHA1withRSA），计算出最后的签名sign，对其base64编码，放入head的key（sign）中。（提供的SHA1withRSAHelper.cs已经包含base64编码了）
            #endregion

            //addPersonalUser();
            //getUser();
            //createContract();

            //sendCode();
            //updateMobileByCode();

            //modifyMobile();

            //updateMobile();

            //发起合同
            //createContract();

            //创建用章
            //createSeal();

            //发起合同
            //createContract();

            // 发起签署合同
            addSigner();

            //查询合同信息
            //getContract();

            //下载合同
            //downloadContract();


        }

        #region 接口测试

        #region 人员新增
        public static void addPersonalUser()
        {

            //用户接口-添加个人用户
            string AddPersonalUserUrl = requestUrl+"/user/addPersonalUser";

            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("idCardType", "1");//证件类型1：居民身份证，2：台湾居民来往内地通行证，3：港澳居民往来内地通行证，10：武装警察身份证；11：军人身份证；15：警察（警官）证；21：外国人永久居留证；23：护照
            dicContent.Add("name", "AAAA");//姓名
            dicContent.Add("account", "account001");//用户唯一识别码（可用证件号、手机号等标识）
            dicContent.Add("mobile", "18055885555");//手机号（用于接收短信验证码）
            dicContent.Add("idCard", "450981199406083635");//个人身份证号、台胞证、港澳通行证等
            dicContent.Add("signPwd", "123");//签约密码明文，如果为空即由我方随机生成签署密码
            dicContent.Add("identifyType",1);//实名认证类型:1：身份证两要素；2：运营商三要素认证；3：银行卡四要素认证；
            dicContent.Add("identifyMobile", "13193284964");//认证使用手机号
            dicContent.Add("bankCard", "");//认证使用银行账号，银行卡四要素使用
            dicContent.Add("isNotice", "1");//开启短信通知(用户发起合同时和需要发送签署密码通知时), 0：否(默认) 1：是
            dicContent.Add("isSignPwdNotice", "1");//0:不通知 1：通知（是否将签署密码以短信的方式发送给用户）

            requestCommon(AddPersonalUserUrl, dicContent);
            Console.ReadKey();

        }
        #region MyRegion
        
        
        //查询用户
        public static void getUser() 
        {
            string url = requestUrl+"/user/getUser";
            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("account", "account001");//用户唯一识别码
            requestCommon(url, dicContent);
            Console.ReadKey();
        }

        public static void updateMobileByCode() {
            JObject info = sendCode();
            string token = info["data"]["codeToken"].ToString();
            Console.WriteLine("token=====" + token);

            Console.Write("请输入验证码：");
            string code = Console.ReadLine();
            
            string newMobile = "13193284964";

            newMobile = "13926979302";
            modifyMobileByCode(newMobile, code.ToString(), token);
            Console.ReadKey();
            
        }

        //修改用户绑定手机号(验证方式)
        public static void modifyMobileByCode(string newMobile,string code,string token)
        {
            string url = requestUrl+"/user/modifyMobileByCode";
            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("account", "account001");//用户唯一识别码
            dicContent.Add("mobile", newMobile);//用户修改后绑定手机号码
            dicContent.Add("code", code);//用户原手机号接收验证码
            dicContent.Add("ctoken", token);//提交用户信息接口回传token
            requestCommon(url, dicContent);
            Console.ReadKey();
        }
        //发送验证码（修改用户绑定手机号）
        public static JObject sendCode()
        {
            string url = requestUrl + "/user/sendCode";
            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("account", "account001");//用户唯一识别码

            return  requestCommon(url, dicContent);
            
        }
        //修改手机号（运营商三要素一致性验证）
        public static void modifyMobile()
        {
            string url = requestUrl + "/user/modifyMobile";
            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("account", "1");//用户唯一识别码
            dicContent.Add("name", "1");//姓名
            dicContent.Add("idCard", "1");//个人身份证号
            dicContent.Add("mobile", "1");//用户修改后绑定手机号码
            requestCommon(url, dicContent);
            Console.ReadKey();
        }
        //修改手机号（无需运营商认证）
        public static void updateMobile()
        {
            string url = requestUrl + "/user/updateMobile";
            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("account", "account001");//用户唯一识别码
            dicContent.Add("idCard", "450981199406083635");//个人身份证号
            dicContent.Add("mobile", "13193284964");//用户修改后绑定手机号码
            requestCommon(url, dicContent);
            Console.ReadKey();
        }

        //修改签约密码
        public static void signPasswd()
        {
            string url = requestUrl + "/user/signPasswd";
            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("account", "1");//用户唯一识别码
            dicContent.Add("oldSignPwd", "1");//修改前的签署密码
            dicContent.Add("signPwd", "1");//修改后的签署密码
            requestCommon(url, dicContent);
            Console.ReadKey();
        }
        //初始化签署密码
        public static void startModSignPasswd()
        {
            string url = requestUrl + "/user/startModSignPasswd";
            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("account", "1");//用户唯一识别码
            requestCommon(url, dicContent);
            Console.ReadKey();
        }
        #endregion
        #endregion


        #region 签章
        public static void createSeal()
        {
            string url = requestUrl + "/user/startModSignPasswd";
            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("account", "account001");//用户唯一识别码
            dicContent.Add("isDefault", "0");//是否默认签，0：否 1：是
            //dicContent.Add("sealName", "签章1");//印章名称 (60个字符以内)
            dicContent.Add("sealName", "签章2");//印章名称 (60个字符以内)
            //dicContent.Add("sealNo", "123");//印章编号 (32个字符)
            dicContent.Add("sealNo", "234");//印章编号 (32个字符)
            List<FileInfo> fileInfoList = new List<FileInfo>();
            //FileInfo fileInfo = new FileInfo("E:\\A2022检测平台\\电子签章厂家\\C#\\Demo\\Demo\\Demo\\pdf\\1.png");
            FileInfo fileInfo = new FileInfo("E:\\A2022检测平台\\电子签章厂家\\C#\\Demo\\Demo\\Demo\\pdf\\2.jpg");
            fileInfoList.Add(fileInfo);
            dicContent.Add("image", fileInfoList);
            dicContent.Add("scaling", 1);
            requestCommon(url, dicContent, fileInfoList);
            Console.ReadKey();
        }
        #endregion

        #region 合同
        //上传合同
        public static void createContract()
        {

            var formDatas = new List<Demo.FormItemModel>();
            //创建合同
            string Url = "http://prev.acsign.cn/contract/createContract";

            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            //dicContent.Add("contractNo", "测试contract01");
            dicContent.Add("contractNo", "contract02");
            dicContent.Add("contractName", "测试合同1");
            dicContent.Add("validityTime", 2);
            dicContent.Add("signOrder", 1);

            List<FileInfo> fileInfoList = new List<FileInfo>();
            FileInfo fileInfo = new FileInfo("E:\\A2022检测平台\\电子签章厂家\\C#\\Demo\\Demo\\Demo\\pdf\\B111_Word.pdf");
            fileInfoList.Add(fileInfo);
            dicContent.Add("contractFiles", fileInfoList);

            requestCommon(Url, dicContent, "contractFiles", fileInfoList);

            Console.ReadKey();

        }
        /// <summary>
        /// 发起签署合同
        /// </summary>
        public static void addSigner()
        {

            var formDatas = new List<Demo.FormItemModel>();
            //发起签署合同
            string Url = "http://prev.acsign.cn/contract/addSigner";

            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("contractNo", "contract02");// 创建合同时对应的编号
            dicContent.Add("account", "account001");//唯一识别码
            dicContent.Add("signType", 2);//签约方式 2-无感知签章 3-有感知签章
            dicContent.Add("sealNo", "123");//否 印章编号（32位之内）
            dicContent.Add("noticeMobile", "13193284964");//否 签约短信通知号码
            //dicContent.Add("noticeEmail", "");//否 签约短信通知邮箱号
            dicContent.Add("signOrder", "1");//否 使用顺序签约时签约顺序编号,无序签约都是1
            dicContent.Add("validateType", 1);//否 1:短信验证码签约，2：签约密码签约，3：人脸识别签约，6：手写识别签名+短信签约，7：手写签名+短信签约，8：手写签名+人脸识别签约，9：手写识别签名+人脸识别签约
            dicContent.Add("autoSwitch", 0);//否 默认自动切换，0：不切换
            dicContent.Add("isNoticeComplete", "1");//否   合同全部签署完成后是否通知用户，0：否 1：是
            dicContent.Add("needPwdSignH5", "0");//否   0：否(默认) 1：是 是否需要密码签署页
            //dicContent.Add("waterMark", "1");//否   0：否（默认） 1：是，是否添加日期水印（距离底部10px）的正中央位置
            
            //dicContent.Add("signStrikeList", List<Object>);//是   在合同附件中骑缝章策略（仅支持企业用户）

            List<signStrategy> signStrategyList = new List<signStrategy>();
            signStrategy signStrategyInfo = new signStrategy();
            signStrategyInfo.attachNo = 1;
            signStrategyInfo.locationMode = "2";
            signStrategyInfo.signPage = 0;
            signStrategyInfo.signX = 10.00;
            signStrategyInfo.signY = 10.00;
            signStrategyList.Add(signStrategyInfo);
            dicContent.Add("signStrategyList", signStrategyList);//是   在合同附件中签约策略
            
            requestCommon(Url, dicContent);
            Console.ReadKey();

        }

        /// <summary>
        /// 查询合同信息
        /// </summary>
        public static void getContract()
        {
            //用户接口-添加个人用户
            string Url = requestUrl + "/contract/getContract";

            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("contractNo", "contract02");//合同编号

            requestCommon(Url, dicContent);
            Console.ReadKey();
        }

        /// <summary>
        /// 下载
        /// </summary>
        public static void downloadContract()
        {
            //用户接口-添加个人用户
            string Url = requestUrl + "/contract/downloadContract";

            //请求体
            Dictionary<string, object> dicContent = new Dictionary<string, object>();
            dicContent.Add("contractNo", "contract02");//合同编号

            requestCommon(Url, dicContent);
            Console.ReadKey();
        }


        #endregion


        #endregion

        #region 公共请求
        /// <summary>
        /// 公共请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="requestContent">请求参数</param>
        public static JObject requestCommon(string url, Dictionary<string, object> requestContent)
        {
            var formDatas = new List<Demo.FormItemModel>();
            //（时间戳，13位，还要+10分钟）
            string timestamp = GetTimeStamp().ToString();
            Console.WriteLine("timestamp======" + timestamp);

            //json按照阿拉伯字母排序(如：{"ba":1,"ac":2}--->{"ac":2,"ba":1})，
            //string dicOrderby = "{" + string.Join(",", requestContent.OrderBy(p => p.Key).Select(p => "\"" + p.Key + "\":\"" + p.Value + "\"")) + "}";

            SortedDictionary<string, object> sordic = KeySort(JObject.Parse(JsonConvert.SerializeObject(requestContent)));
            string dicOrderby = JsonConvert.SerializeObject(sordic);

            //发起签署合同->数组
            dicOrderby = "[" + dicOrderby + "]";

            Console.WriteLine("--------" + JsonConvert.DeserializeObject( dicOrderby));

            //生成签名
            Console.WriteLine("dicOrderby======" + dicOrderby);
            Console.WriteLine();
            string rsaSuffix = dicOrderby + md5(dicOrderby) + APPID + timestamp;
            Console.WriteLine("md5====" + md5(dicOrderby));


            string sign = SHA1withRSAHelper.sign(rsaSuffix, privateKey, "utf-8");
            Console.WriteLine("sign======" + sign);
            //请求头部
            Dictionary<string, string> dicHeader = new Dictionary<string, string>();
            dicHeader.Add("sign", sign);
            dicHeader.Add("timestamp", timestamp);

            //添加文本
            formDatas.Add(new Demo.FormItemModel()
            {
                Key = "appId",
                Value = APPID
            });
            formDatas.Add(new Demo.FormItemModel()
            {
                Key = "timestamp",
                Value = timestamp
            });
            
            formDatas.Add(new Demo.FormItemModel()
            {
                Key = "bizData",
                Value = dicOrderby
            });

            JObject Info = (JObject)JsonConvert.DeserializeObject(PostForm(url, dicHeader, formDatas));
            Console.WriteLine(Info);

            return Info;
            

        }

        public static JObject requestCommon(string url, Dictionary<string, object> requestContent, List<FileInfo> fileInfoList)
        {
            var formDatas = new List<Demo.FormItemModel>();
            //（时间戳，13位，还要+10分钟）
            string timestamp = GetTimeStamp().ToString();
            Console.WriteLine("timestamp======" + timestamp);

            //json按照阿拉伯字母排序(如：{"ba":1,"ac":2}--->{"ac":2,"ba":1})，
            //string dicOrderby = "{" + string.Join(",", requestContent.OrderBy(p => p.Key).Select(p => "\"" + p.Key + "\":\"" + p.Value + "\"")) + "}";

            SortedDictionary<string, object> sordic = KeySort(JObject.Parse(JsonConvert.SerializeObject(requestContent)));
            string dicOrderby = JsonConvert.SerializeObject(sordic);


            //string dicOrderby = "{\"account\":\"account001\",\"bankCard\":\"\",\"idCard\":\"450981199406083635\",\"idCardType\":\"1\",\"identifyMobile\":\"13193284964\",\"identifyType\":1,\"isNotice\":\"1\",\"isSignPwdNotice\":\"1\",\"mobile\":\"13193284964\",\"name\":\"林健\",\"signPwd\":\"123\"}";

            //生成签名
            Console.WriteLine("dicOrderby======" + dicOrderby);
            string rsaSuffix = dicOrderby + md5(dicOrderby) + APPID + timestamp;
            Console.WriteLine("md5====" + md5(dicOrderby));


            string sign = SHA1withRSAHelper.sign(rsaSuffix, privateKey, "utf-8");
            Console.WriteLine("sign======" + sign);
            //请求头部
            Dictionary<string, string> dicHeader = new Dictionary<string, string>();
            dicHeader.Add("sign", sign);
            dicHeader.Add("timestamp", timestamp);

            //添加文本
            formDatas.Add(new Demo.FormItemModel()
            {
                Key = "appId",
                Value = APPID
            });
            formDatas.Add(new Demo.FormItemModel()
            {
                Key = "timestamp",
                Value = timestamp
            });
            formDatas.Add(new Demo.FormItemModel()
            {
                Key = "bizData",
                Value = dicOrderby
            });
            //formDatas.Add(new FormItemModel()
            //{
            //    Key = "log1",
            //    Value = "",
            //    FileName = "log1",
            //    FileContent = File.OpenRead(log1)
            //});
            foreach (var fileInfo in fileInfoList)
            {
                formDatas.Add(new Demo.FormItemModel()
                {
                    Key = fileInfo.Name.Split(',')[0].ToString(),
                    Value = "",
                    FileName = fileInfo.Name.Split(',')[0].ToString(),
                    FileContent = File.OpenRead(fileInfo.FullName)
                });
            }

            JObject Info = (JObject)JsonConvert.DeserializeObject(PostForm(url, dicHeader, formDatas));
            Console.WriteLine(Info);

            return Info;


        }

        public static JObject requestCommon(string url, Dictionary<string, object> requestContent,string FilesKey, List<FileInfo> fileInfoList)
        {
            var formDatas = new List<Demo.FormItemModel>();
            //（时间戳，13位，还要+10分钟）
            string timestamp = GetTimeStamp().ToString();
            Console.WriteLine("timestamp======" + timestamp);

            //json按照阿拉伯字母排序(如：{"ba":1,"ac":2}--->{"ac":2,"ba":1})，
            //string dicOrderby = "{" + string.Join(",", requestContent.OrderBy(p => p.Key).Select(p => "\"" + p.Key + "\":\"" + p.Value + "\"")) + "}";

            SortedDictionary<string, object> sordic = KeySort(JObject.Parse(JsonConvert.SerializeObject(requestContent)));
            string dicOrderby = JsonConvert.SerializeObject(sordic);


            //string dicOrderby = "{\"account\":\"account001\",\"bankCard\":\"\",\"idCard\":\"450981199406083635\",\"idCardType\":\"1\",\"identifyMobile\":\"13193284964\",\"identifyType\":1,\"isNotice\":\"1\",\"isSignPwdNotice\":\"1\",\"mobile\":\"13193284964\",\"name\":\"林健\",\"signPwd\":\"123\"}";

            //生成签名
            Console.WriteLine("dicOrderby======" + dicOrderby);
            string rsaSuffix = dicOrderby + md5(dicOrderby) + APPID + timestamp;
            Console.WriteLine("md5====" + md5(dicOrderby));


            string sign = SHA1withRSAHelper.sign(rsaSuffix, privateKey, "utf-8");
            Console.WriteLine("sign======" + sign);
            //请求头部
            Dictionary<string, string> dicHeader = new Dictionary<string, string>();
            dicHeader.Add("sign", sign);
            dicHeader.Add("timestamp", timestamp);

            //添加文本
            formDatas.Add(new Demo.FormItemModel()
            {
                Key = "appId",
                Value = APPID
            });
            formDatas.Add(new Demo.FormItemModel()
            {
                Key = "timestamp",
                Value = timestamp
            });
            formDatas.Add(new Demo.FormItemModel()
            {
                Key = "bizData",
                Value = dicOrderby
            });
            //formDatas.Add(new FormItemModel()
            //{
            //    Key = "log1",
            //    Value = "",
            //    FileName = "log1",
            //    FileContent = File.OpenRead(log1)
            //});
            foreach (var fileInfo in fileInfoList)
            {
                formDatas.Add(new Demo.FormItemModel()
                {
                    Key = FilesKey,
                    Value = "",
                    FileName = fileInfo.Name.Split(',')[0].ToString(),
                    FileContent = File.OpenRead(fileInfo.FullName)
                });
            }

            JObject Info = (JObject)JsonConvert.DeserializeObject(PostForm(url, dicHeader, formDatas));
            Console.WriteLine(Info);

            return Info;


        }

        #endregion

        #region GET请求与获取结果
        /// <summary>  
        /// GET请求与获取结果  
        /// </summary>  
        public static string HttpGet(string Url, string postDataStr)
        {
            //LogHelper.ShowMsg("Url：" + Url + "|||||||postDataStr:" + postDataStr);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        #endregion

        #region POST请求与获取结果
        /// <summary>  
        /// POST请求与获取结果  
        /// </summary>  
        public static string HttpPost(string Url,  Dictionary<string, string> dicHeader, Dictionary<string, string> dicContent)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = " multipart/form-data;";
            request.Timeout = 50000;

            #region 头部
            if (dicHeader != null && dicHeader.Count != 0)
            {
                foreach (var item in dicHeader)
                {
                    request.Headers.Add(item.Key.ToString(), item.Value.ToString());
                }
            }
            #endregion
            #region 添加Post 参数

            //StringBuilder builder = new StringBuilder();
            //int i = 0;
            //foreach (var item in dicContent)
            //{
            //    if (i > 0)
            //        builder.Append("&");
            //    builder.AppendFormat("{0}={1}", item.Key, item.Value);
            //    i++;
            //}

            
            //byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            //request.ContentLength = data.Length;
            //using (Stream reqStream = request.GetRequestStream())
            //{
            //    reqStream.Write(data, 0, data.Length);
            //    reqStream.Close();
            //}
            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }

        ///// <summary>  
        ///// POST请求与获取结果  
        ///// </summary>  
        //public static string HttpPost(string Url, string postDataStr)
        //{
        //    return HttpPost(Url, postDataStr, new Dictionary<string, string>() { });
        //}

        ///// <summary>  
        ///// POST请求与获取结果  
        ///// </summary>  
        //public static string HttpPost(string Url)
        //{
        //    return HttpPost(Url, "", new Dictionary<string, string>() { });
        //}
        #endregion

        #region 生成时间戳
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {

            DateTime timeStampStartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long timeStamp = (long)(DateTime.Now.AddMinutes(10).ToUniversalTime() - timeStampStartTime).TotalMilliseconds ;

            return timeStamp;

        }
        #endregion

        #region MD5加密
        
        public static string md5(string str)
        {
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] bytValue, bytHash;
                bytValue = System.Text.Encoding.UTF8.GetBytes(str);

                bytHash = MD5.Create().ComputeHash(bytValue);
                md5.Clear();



                string sTemp = "";
                for (int i = 0; i < bytHash.Length; i++)
                {
                    sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
                }
                str = sTemp.ToLower();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return str;
        }

        #endregion

        #region form-data post
        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formItems">Post表单内容</param>
        /// <param name="cookieContainer"></param>
        /// <param name="timeOut">默认20秒</param>
        /// <param name="encoding">响应内容的编码类型（默认utf-8）</param>
        /// <returns></returns>
        public static string PostForm(string url, Dictionary<string, string> dicHeader, List<FormItemModel> formItems, CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 20000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            #region 初始化请求对象
            request.Method = "POST";
            request.Timeout = timeOut;
            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Accept = "Application/json;";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            if (!string.IsNullOrEmpty(refererUrl))
                request.Referer = refererUrl;
            if (cookieContainer != null)
                request.CookieContainer = cookieContainer;
            #endregion

            #region 头部
            if (dicHeader != null && dicHeader.Count != 0)
            {
                foreach (var item in dicHeader)
                {
                    request.Headers.Add(item.Key.ToString(), item.Value.ToString());
                }
            }
            #endregion

            string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            //请求流
            var postStream = new MemoryStream();
            #region 处理Form表单请求内容
            //是否用Form上传文件
            var formUploadFile = formItems != null && formItems.Count > 0;
            if (formUploadFile)
            {
                //文件数据模板
                string fileFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                    "\r\nContent-Type: application/octet-stream" +
                    "\r\n\r\n";
                //文本数据模板
                string dataFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                    "\r\n\r\n{1}";
                foreach (var item in formItems)
                {
                    string formdata = null;
                    if (item.IsFile)
                    {
                        //上传文件
                        formdata = string.Format(
                            fileFormdataTemplate,
                            item.Key, //表单键
                            item.FileName);
                    }
                    else
                    {
                        //上传文本
                        formdata = string.Format(
                            dataFormdataTemplate,
                            item.Key,
                            item.Value);
                    }

                    //统一处理
                    byte[] formdataBytes = null;
                    //第一行不需要换行
                    if (postStream.Length == 0)
                        formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                    else
                        formdataBytes = Encoding.UTF8.GetBytes(formdata);
                    postStream.Write(formdataBytes, 0, formdataBytes.Length);

                    //写入文件内容
                    if (item.FileContent != null && item.FileContent.Length > 0)
                    {
                        using (var stream = item.FileContent)
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead = 0;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                postStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
                //结尾
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);

            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream.Length;

            #region 输入二进制流
            if (postStream != null)
            {
                postStream.Position = 0;
                //直接写入流
                Stream requestStream = request.GetRequestStream();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                ////debug
                //postStream.Seek(0, SeekOrigin.Begin);
                //StreamReader sr = new StreamReader(postStream);
                //var postStr = sr.ReadToEnd();
                postStream.Close();//关闭文件访问
            }
            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

        #region MyRegion
        
        //    public static string PostForm(string url, string sign, string timestamp, List<FormItemModel> formItems, CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 20000)
    //    {
    //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
    //        #region 初始化请求对象
    //        request.Method = "POST";
    //        request.Timeout = timeOut;
    //        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
    //        //request.Accept = "Application/json;";
    //        request.KeepAlive = true;
    //        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
    //        if (!string.IsNullOrEmpty(refererUrl))
    //            request.Referer = refererUrl;
    //        if (cookieContainer != null)
    //            request.CookieContainer = cookieContainer;
    //        #endregion

    //        #region 头部
    //        request.Headers.Add("sign", sign);
    //        request.Headers.Add("timestamp", timestamp);
    //        #endregion

    //        string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符
    //        request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
    //        //请求流
    //        var postStream = new MemoryStream();
    //        #region 处理Form表单请求内容
    //        //是否用Form上传文件
    //        var formUploadFile = formItems != null && formItems.Count > 0;
    //        if (formUploadFile)
    //        {
    //            //文件数据模板
    //            string fileFormdataTemplate =
    //                "\r\n--" + boundary +
    //                "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
    //                "\r\nContent-Type: application/octet-stream" +
    //                "\r\n\r\n";
    //            //文本数据模板
    //            string dataFormdataTemplate =
    //                "\r\n--" + boundary +
    //                "\r\nContent-Disposition: form-data; name=\"{0}\"" +
    //                "\r\n\r\n{1}";
    //            foreach (var item in formItems)
    //            {
    //                string formdata = null;
    //                if (item.IsFile)
    //                {
    //                    //上传文件
    //                    formdata = string.Format(
    //                        fileFormdataTemplate,
    //                        item.Key, //表单键
    //                        item.FileName);
    //                }
    //                else
    //                {
    //                    //上传文本
    //                    formdata = string.Format(
    //                        dataFormdataTemplate,
    //                        item.Key,
    //                        item.Value);
    //                }

    //                //统一处理
    //                byte[] formdataBytes = null;
    //                //第一行不需要换行
    //                if (postStream.Length == 0)
    //                    formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
    //                else
    //                    formdataBytes = Encoding.UTF8.GetBytes(formdata);
    //                postStream.Write(formdataBytes, 0, formdataBytes.Length);

    //                //写入文件内容
    //                if (item.FileContent != null && item.FileContent.Length > 0)
    //                {
    //                    using (var stream = item.FileContent)
    //                    {
    //                        byte[] buffer = new byte[1024];
    //                        int bytesRead = 0;
    //                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
    //                        {
    //                            postStream.Write(buffer, 0, bytesRead);
    //                        }
    //                    }
    //                }
    //            }
    //            //结尾
    //            var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
    //            postStream.Write(footer, 0, footer.Length);

    //        }
    //        else
    //        {
    //            request.ContentType = "application/x-www-form-urlencoded";
    //        }
    //        #endregion

    //        request.ContentLength = postStream.Length;

    //        #region 输入二进制流
    //        if (postStream != null)
    //        {
    //            postStream.Position = 0;
    //            //直接写入流
    //            Stream requestStream = request.GetRequestStream();

    //            byte[] buffer = new byte[1024];
    //            int bytesRead = 0;
    //            while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
    //            {
    //                requestStream.Write(buffer, 0, bytesRead);
    //            }

    //            ////debug
    //            //postStream.Seek(0, SeekOrigin.Begin);
    //            //StreamReader sr = new StreamReader(postStream);
    //            //var postStr = sr.ReadToEnd();
    //            postStream.Close();//关闭文件访问
    //        }
    //        #endregion

    //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //        if (cookieContainer != null)
    //        {
    //            response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
    //        }

    //        using (Stream responseStream = response.GetResponseStream())
    //        {
    //            using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
    //            {
    //                string retString = myStreamReader.ReadToEnd();
    //                return retString;
    //            }
    //        }
    //    }


    //    public static string PostForm(string url, List<FormItemModel> formItems, string sign, string timestamp, CookieContainer cookieContainer = null,
    //string refererUrl = null, Encoding encoding = null, int timeOut = 20000)
    //    {
    //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
    //        #region 初始化请求对象
    //        request.Method = "POST";
    //        request.Timeout = timeOut;
    //        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
    //        request.KeepAlive = true;
    //        request.Headers.Add("sign", sign);
    //        request.Headers.Add("timestamp", timestamp);
    //        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
    //        if (!string.IsNullOrEmpty(refererUrl))
    //            request.Referer = refererUrl;
    //        if (cookieContainer != null)
    //            request.CookieContainer = cookieContainer;
    //        #endregion

    //        string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符
    //        request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
    //        //请求流
    //        var postStream = new MemoryStream();
    //        #region 处理Form表单请求内容
    //        //是否用Form上传文件
    //        var formUploadFile = formItems != null && formItems.Count > 0;
    //        if (formUploadFile)
    //        {
    //            //文件数据模板
    //            string fileFormdataTemplate =
    //                "\r\n--" + boundary +
    //                "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
    //                "\r\nContent-Type: application/octet-stream" +
    //                "\r\n\r\n";
    //            //文本数据模板
    //            string dataFormdataTemplate =
    //                "\r\n--" + boundary +
    //                "\r\nContent-Disposition: form-data; name=\"{0}\"" +
    //                "\r\n\r\n{1}";
    //            foreach (var item in formItems)
    //            {
    //                string formdata = null;
    //                if (item.IsFile)
    //                {
    //                    //上传文件
    //                    formdata = string.Format(
    //                        fileFormdataTemplate,
    //                        item.Key, //表单键
    //                        item.FileName);
    //                }
    //                else
    //                {
    //                    //上传文本
    //                    formdata = string.Format(
    //                        dataFormdataTemplate,
    //                        item.Key,
    //                        item.Value);
    //                }

    //                //统一处理
    //                byte[] formdataBytes = null;
    //                //第一行不需要换行
    //                if (postStream.Length == 0)
    //                    formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
    //                else
    //                    formdataBytes = Encoding.UTF8.GetBytes(formdata);
    //                postStream.Write(formdataBytes, 0, formdataBytes.Length);

    //                //写入文件内容
    //                if (item.FileContent != null && item.FileContent.Length > 0)
    //                {
    //                    using (var stream = item.FileContent)
    //                    {
    //                        byte[] buffer = new byte[1024];
    //                        int bytesRead = 0;
    //                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
    //                        {
    //                            postStream.Write(buffer, 0, bytesRead);
    //                        }
    //                    }
    //                }
    //            }
    //            //结尾
    //            var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
    //            postStream.Write(footer, 0, footer.Length);

    //        }
    //        else
    //        {
    //            request.ContentType = "application/x-www-form-urlencoded";
    //        }
    //        #endregion

    //        request.ContentLength = postStream.Length;

    //        #region 输入二进制流
    //        if (postStream != null)
    //        {
    //            postStream.Position = 0;
    //            //直接写入流
    //            Stream requestStream = request.GetRequestStream();

    //            byte[] buffer = new byte[1024];
    //            int bytesRead = 0;
    //            while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
    //            {
    //                requestStream.Write(buffer, 0, bytesRead);
    //            }

    //            ////debug
    //            //postStream.Seek(0, SeekOrigin.Begin);
    //            //StreamReader sr = new StreamReader(postStream);
    //            //var postStr = sr.ReadToEnd();
    //            postStream.Close();//关闭文件访问
    //        }
    //        #endregion

    //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //        if (cookieContainer != null)
    //        {
    //            response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
    //        }

    //        using (Stream responseStream = response.GetResponseStream())
    //        {
    //            using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
    //            {
    //                string retString = myStreamReader.ReadToEnd();
    //                return retString;
    //            }
    //        }
        //    }
        #endregion

        #endregion

        #region json排序(升序)
        public static SortedDictionary<string, object> KeySort(JObject obj)
        {
            var res = new SortedDictionary<string, object>();
            foreach (var x in obj)
            {
                if (x.Value is JValue) res.Add(x.Key, x.Value);
                else if (x.Value is JObject) res.Add(x.Key, KeySort((JObject)x.Value));
                else if (x.Value is JArray)
                {
                    var tmp = new SortedDictionary<string, object>[x.Value.Count()];
                    for (var i = 0; i < x.Value.Count(); i++)
                    {
                        tmp[i] = KeySort((JObject)x.Value[i]);
                    }
                    res.Add(x.Key, tmp);
                }
            }
            return res;
        }
        #endregion
    }
}
