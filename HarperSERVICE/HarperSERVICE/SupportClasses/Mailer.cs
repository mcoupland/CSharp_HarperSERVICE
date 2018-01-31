using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using HarperLINQ;
using System.Net.Mail;
using System.Configuration;

namespace SupportClasses
{
    public class Mailer
    {
        public bool SendEmail(string username, string password, string subject, string fromaddress, string toaddress, string cc, string bcc, string body, bool isbodyhtml, string smtp)
        {
            if (username == ConfigurationManager.AppSettings["mailserviceuser"]
                && password == ConfigurationManager.AppSettings["mailservicepwd"])
            {
         
                HarperLINQ.SimpleMail mail = new SimpleMail();
                try
                {
                    mail.bccemail = bcc;
                    mail.body = body;
                    mail.ccemail = cc;
                    mail.datecreated = DateTime.Now;
                    mail.fromemail = fromaddress;
                    mail.isHtml = isbodyhtml;
                    mail.ishtml = isbodyhtml;
                    mail.smtpAddress = smtp;
                    mail.subject = subject;
                    mail.toemail = toaddress;
                    using (AHT_MainDataContext context = new AHT_MainDataContext(ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString))
                    {
                        context.SimpleMails.InsertOnSubmit(mail);
                        context.SubmitChanges();
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else { return false; }
        }

        public static string ForgotPasswordResetBody(string user, string pwd)
        {
            StringBuilder email = new StringBuilder();

            #region Create email text and insert variables
            email.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            email.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            email.Append("<head>");
            email.Append("    <base target=\"_blank\" />");
            email.Append("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />");
            email.Append("    <title>Your Featured Travel Values | News From Andrew Harper</title>");
            email.Append("</head>");
            email.Append("<head>");
            email.Append("    <style type=\"text/css\">");
            email.Append("        body");
            email.Append("        {");
            email.Append("            font-family: Georgia, serif;");
            email.Append("            background-color: #eee;");
            email.Append("        }");
            email.Append("        abbr");
            email.Append("        {");
            email.Append("            font-variant: small-caps;");
            email.Append("        }");
            email.Append("        a:active, a:visited, a:link");
            email.Append("        {");
            email.Append("            color: #5c8727;");
            email.Append("            text-decoration: none;");
            email.Append("        }");
            email.Append("        a:hover");
            email.Append("        {");
            email.Append("            color: #5c8727;");
            email.Append("            text-decoration: underline;");
            email.Append("        }");
            email.Append("        .GrayHead");
            email.Append("        {");
            email.Append("            color: #808086;");
            email.Append("            letter-spacing: 1px;");
            email.Append("        }");
            email.Append("    </style>");
            email.Append("</head>");
            email.Append("<body>");
            email.Append("    <div align=\"center\" style=\"font-size: 8pt; line-height: 1.3; padding-bottom: 3px;\">");
            email.Append("        <font face=\"Verdana,sans-serif\" color=\"#000\"><strong>Andrew Harper Password Reminder</strong><br />");
            email.Append("            Follow us on <a href=\"http://www.facebook.com/pages/Andrew-Harper/70674486854?sid=7bb23ae8202668ee9c3ba769e85d4c4c&amp;ref=search\"");
            email.Append("                style=\"color: #5c8727; text-decoration: underline;\" target=\"_blank\">Facebook</a>");
            email.Append("            and <a href=\"http://twitter.com/HarperTravel\" style=\"color: #5c8727; text-decoration: underline;\"");
            email.Append("                target=\"_blank\">Twitter</a><br />");
            email.Append("            Please add <a href=\"mailto:ah.email@andrewharper.com\" style=\"color: #5c8727; text-decoration: underline;\">");
            email.Append("                ah.email@andrewharper.com</a> to your address book to ensure that our emails");
            email.Append("            reach your inbox. </font>");
            email.Append("    </div>");
            email.Append("    <table width=\"600\" align=\"center\" cellpadding=\"3\" cellspacing=\"0\">");
            email.Append("        <tr bgcolor=\"#ffffff\">");
            email.Append("            <td bgcolor=\"#ffffff\">");
            email.Append("                <table cellpadding=\"3\" cellspacing=\"0\" width=\"600\">");
            email.Append("                    <tr bgcolor=\"#FFFFFF\">");
            email.Append("                        <td>");
            email.Append("                            <table border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"8\">");
            email.Append("                                <tr valign=\"top\" align=\"left\">");
            email.Append("                                    <td width=\"600\" height=\"60\">");
            email.Append("                                        <div align=\"center\" style=\"border-bottom: 1px solid #CCCCCC;\">");
            email.Append("                                            <p>");
            email.Append("                                                <a href=\"https://www.andrewharper.com/Member/Default.aspx\">");
            email.Append("                                                    <img src=\"http://www.andrewharper.com/ImageStore/images/Enews/headerbird.gif\" alt=\"Andrew Harper\"");
            email.Append("                                                        width=\"185\" height=\"67\" vspace=\"3\" border=\"0\" align=\"top\" />");
            email.Append("                                                </a><span style=\"font-size: 11.5px; font-family: Verdana; color: #333333; line-height: 1.4em;\">");
            email.Append("                                                    <br />");
            email.Append("                                                    Book by phone: (866) 831-4314 | Book by email: <a href=\"mailto:Reservations@AndrewHarper.com\"");
            email.Append("                                                        style=\"color: #5c8727;\">Reservations@AndrewHarper.com</a></span></p>");
            email.Append("                                        </div>");
            email.Append("                                    </td>");
            email.Append("                                </tr>");
            email.Append("                                <tr align=\"left\" valign=\"top\" bgcolor=\"#FFFFFF\">");
            email.Append("                                    <td align=\"center\">");
            email.Append("                                    </td>");
            email.Append("                                </tr>");
            email.Append("                                <tr>");
            email.Append("                                    <td align=\"center\">");
            email.Append("                                        <span style=\"font-size: 18px; font-family: Georgia, serif; color: #333333; line-height: 1.4em;\">");
            email.Append("                                            Andrew Harper Password Reminder </span>");
            email.Append("                                        <div style=\"font-size: 13px; font-family: Verdana, Geneva, sans-serif; color: #333333;");
            email.Append("                                            line-height: 1.4em; padding-top: 3px; border-bottom: 1px solid #CCCCCC;\">");
            email.Append("                                            <p>");
            email.Append("                                                Below is your login account information:<br />");
            email.Append("                                                <br />");
            email.Append("                                                <strong>User Name:</strong>");
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            email.Append(user);
            email.Append("                                                <br />");
            email.Append("                                                <strong>Password:</strong>");
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            // ******************************************************************************************************************
            email.Append(pwd);
            email.Append("                                                <br />");
            email.Append("                                                <br />");
            email.Append("                                                Please <a href=\"https://www.andrewharper.com/Member/login.aspx\"><strong>CLICK HERE</strong></a>");
            email.Append("                                                to login.");
            email.Append("                                            </p>");
            email.Append("                                            <p>");
            email.Append("                                                If you need further assistance, please contact our Membership Office");
            email.Append("                                                        <br />");
            email.Append("                                                at <strong>(866) 831-4314 or (630) 734-4610</strong>.<br />");
            email.Append("                                                <br />");
            email.Append("                                            </p>");
            email.Append("                                        </div>");
            email.Append("                                    </td>");
            email.Append("                                </tr>");
            email.Append("                            </table>");
            email.Append("                        </td>");
            email.Append("                    </tr>");
            email.Append("                    <tr>");
            email.Append("                        <td width=\"75%\" valign=\"top\">");
            email.Append("                            <div align=\"center\">");
            email.Append("                                <div align=\"center\">");
            email.Append("                                    <a href=\"http://www.andrewharper.com\">");
            email.Append("                                        <img src=\"http://www.andrewharper.com/ImageStore/Images/icons/harper_icon.gif\" alt=\"Andrew Harper\"");
            email.Append("                                            width=\"35\" height=\"34\" border=\"0\" style=\"border: medium none\" /></a>&nbsp;");
            email.Append("                                    <a href=\"http://www.facebook.com/pages/Andrew-Harper/70674486854?sid=7bb23ae8202668ee9c3ba769e85d4c4c&amp;ref=search\"");
            email.Append("                                        target=\"_blank\">");
            email.Append("                                        <img src=\"http://www.andrewharper.com/ImageStore/Images/icons/icon_facebook.gif\"");
            email.Append("                                            alt=\"Andrew Harper Facebook\" width=\"35\" height=\"34\" border=\"0\" style=\"border: medium none\" />&nbsp;</a>");
            email.Append("                                    <a href=\"http://twitter.com/HarperTravel\" target=\"_blank\">");
            email.Append("                                        <img src=\"http://www.andrewharper.com/ImageStore/Images/icons/icon_twitter.gif\" alt=\"Andrew Harper Twitter\"");
            email.Append("                                            width=\"35\" height=\"34\" border=\"0\" style=\"border: medium none\" />&nbsp;</a>");
            email.Append("                                    <a href=\"http://www.thingsyoushouldknowblog.com/?feed=rss2\" target=\"_blank\">");
            email.Append("                                        <img src=\"http://www.andrewharper.com/ImageStore/Images/icons/icon_rss_blog.gif\"");
            email.Append("                                            alt=\"RSS feed blog\" width=\"35\" height=\"34\" border=\"0\" style=\"border: none\" />&nbsp;</a>");
            email.Append("                                </div>");
            email.Append("                            </div>");
            email.Append("                            <div align=\"center\" style=\"font-size: 8pt; line-height: 150%; margin-bottom: 0; padding: 5px8px 6px 8px;\">");
            email.Append("                                <font face=\"Georgia, serif\" color=\"#000000\">&copy; Andrew Harper Inc. | (866) 831-4314");
            email.Append("                                    or (630) 734-4610 | 1703 West Fifth Street, Suite 800, Austin, TX 78703");
            email.Append("                                    <br />");
            email.Append("                                    View our <a href=\"http://www.andrewharper.com/Consideration/Luxury-Travel/Footers/privacy.aspx\"");
            email.Append("                                        style=\"color: #5c8727;\">Privacy Statement</a> |<a href=\"http://www.andrewharper.com/Consideration/Luxury-Travel/Footers/ContactUs.aspx\"");
            email.Append("                                            style=\"color: #5c8727;\"> Contact Us</a> | <a href=\"http://www.andrewharper.com/Consideration/Luxury-Travel/Footers/TermsConditions.aspx\"");
            email.Append("                                                style=\"color: #5c8727;\">Terms &amp; Conditions</a> </font></font>");
            email.Append("                            </div>");
            email.Append("                        </td>");
            email.Append("                    </tr>");
            email.Append("                </table>");
            email.Append("            </td>");
            email.Append("        </tr>");
            email.Append("    </table>");
            email.Append("</body>");
            email.Append("</html>");
            #endregion

            return email.ToString();
        }
    }
}

