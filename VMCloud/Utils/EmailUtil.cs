/*
*	Author:       xzy
**	Created:      2019/7/22 21:38:17
*   Description:  发送邮件相关
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using VMCloud.Models;
using System.Configuration;
using VMCloud.Utils;
using VMCloud.Models.DAO;
using System.Text;
namespace VMCloud.Utils
{
    public class EmailUtil
    {
        public static string SendEmail(string emailmain, string emailContent, string userid, int hasHref = 0, string href = "")
        {
            try
            {
                string smtpServer = ConfigurationManager.AppSettings["smtpServer"];
                string mailFrom = ConfigurationManager.AppSettings["EmailAccount"];
                string userPassword = ConfigurationManager.AppSettings["EmailPassword"];
                string imagesrc = ConfigurationManager.AppSettings["EmailTitlePic"];

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
                smtpClient.Host = smtpServer; //指定SMTP服务器
                smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);

                //smtpClient.EnableSsl = true;
                smtpClient.Port = 25;
                //string imagesrc = "";
                string Toemail = "";
                User user = UserDao.GetUserById(userid);
                Toemail = user.email;
                if (Toemail == "")
                {
                    return "fail";
                }
                string username = user.name;
                string EmailMain = emailmain;
                string EmailText = "<table align='center' border='0' cellpadding='0' cellspacing='0'style=' width:88%; margin-top:20px; margin-bottom:20px; background:#fafafa; border:1px solid #ddd;'><tbody>"
                               + "<tr>"
                                    + "<td width='24'>&nbsp;</td>"
                                    + "<td style='padding-top:16px;'><img height='50px' src='" + imagesrc + "' style='display:block; vertical-align:top;' width='250px'></td>"
                                + "</tr><tr>"
                                   + "<td width='24'>&nbsp;</td>"
                                   + "<td colspan='2' style='color:#858585; font-family:Arial, Helvetica, sans-serif; font-size:14px; line-height:20px; padding-top:24px;'>" + username + " 您好：</td>"
                                   + "<td width='24'>&nbsp;</td>"
                               + "</tr><tr>"
                                   + "<td width='24'>&nbsp;</td>"
                                   + "<td colspan='2' style='color:#858585; font-family:Arial, Helvetica, sans-serif; font-size:14px; line-height:20px; padding-top:18px;'>" + emailContent + "</td>"
                                   + "<td width='24'>&nbsp;</td>"
                               + "</tr>"
                                   + (hasHref == 1 ? "<tr>" : "")
                                   + (hasHref == 1 ? "<td width='24'>&nbsp;</td>" : "")
                                   + (hasHref == 1 ? "<td colspan='2' style='color:#858585; font-family:Arial, Helvetica, sans-serif; font-size:14px; line-height:20px; padding-top:18px;'><a href='" + href + "' style='color:#50b7f1;text-decoration:none;font-weight:bold' target='_blank'>"+emailmain+"</a></td>" : "")
                                   + (hasHref == 1 ? "<td width='24'>&nbsp;</td>" : "")
                                   + (hasHref == 1 ? "</tr>" : "")
                                   + (hasHref == 1 ? "<tr>" : "")
                                   + (hasHref == 1 ? "<td width='24'>&nbsp;</td>" : "")
                                   + (hasHref == 1 ? "<td colspan='2' style='color:#858585; font-family:Arial, Helvetica, sans-serif; font-size:14px; line-height:20px; padding-top:24px;'>如果上述链接无法点击，请复制以下链接" + href + " </td>" : "")
                                   + (hasHref == 1 ? "<td width='24'>&nbsp;</td>" : "")
                                   + (hasHref == 1 ? "</tr>" : "")
                               + "<tr>"
                                   + "<td style='padding-top:18px; padding-bottom:32px; border-bottom:1px solid #e1e1e1;' width='24'>&nbsp;</td>"
                                   + "<td colspan='2' style='color:#858585; font-family:Arial, Helvetica, sans-serif; font-size:14px; line-height:20px; padding-top:18px; padding-bottom:32px; border-bottom:1px solid #e1e1e1;'>谢谢使用！<br> 北航软院云平台</td>"
                                   + "<td style='padding-top:18px; padding-bottom:32px; border-bottom:1px solid #e1e1e1;' width='24'>&nbsp;</td>"
                               + "</tr>"
                               + "<tr>"
                               + "<td width='24'>&nbsp;</td>"
                                   + "<td colspan='2' style='color:#858585; font-family:Arial, Helvetica, sans-serif; font-size:14px; line-height:20px; padding-top:18px;'>您可以<a href='" + ConfigurationManager.AppSettings["MyServer"] + "' style='color:#50b7f1;text-decoration:none;font-weight:bold' target='_blank'>点击此处访问云平台</a></td>"
                                   + "<td width='24'>&nbsp;</td>"
                                   + "</tr>"
                                   + "<tr>"
                                   + "<td width='24'>&nbsp;</td>"
                                   + "<td colspan='2' style='color:#858585; font-family:Arial, Helvetica, sans-serif; font-size:14px; line-height:20px; padding-top:24px;'>如果上述链接无法点击，请复制以下链接到浏览器地址栏进行访问：" + ConfigurationManager.AppSettings["MyServer"] + " </td>"
                                   + "<td width='24'>&nbsp;</td>"
                                   + "</tr>"
                               + "</tr>"
                           + "</tbody></table>";
                MailMessage mailMessage = new MailMessage(mailFrom, Toemail);
                mailMessage.Subject = EmailMain;
                mailMessage.Body = EmailText;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Low;
                smtpClient.Send(mailMessage);
                return "success";
            }
            catch (SmtpException ex)
            {
                return ex.Message;
            }
        }    

    }

}
