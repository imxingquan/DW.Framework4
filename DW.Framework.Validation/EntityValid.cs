using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace DW.Framework.Validation
{
    /// <summary>
    /// 实体验证类 
    /// </summary>
    /// <example>
    /*
    
    public partial class PUserMeta
    {
        [StringLengthValidator(4, 20, MessageTemplate = "用户名:请确保输入的值在4-20个")]
        [RegexValidator(@"^[\u0391-\uFFE5\w]+$", MessageTemplate = "用户名只能包括中文字、英文字母、数字和下划线")]
        public string UName { get; set; }

        [StringLengthValidator(6, 20, MessageTemplate = "密码长度在6-15位之间")]
        public string Pass { get; set; }

        [RegexValidator(@"[\w-]+@([\w-]+\.)+[\w-]+", MessageTemplate = "电子邮件格式不正确")]
        public string Email { get; set; }
    }

    [MetadataType(typeof(PUserMeta))]
    public partial class PUser
    {

        [PropertyComparisonValidator("Pass", ComparisonOperator.Equal, MessageTemplate = "两次输入的密码不相同")]
        public string Pass2 { get; set; }
        [NotNullValidator(MessageTemplate = "请输入验证码")]
        public string ValidCode { get; set; }
    }
    
     
     
     DDM.UCenter.Entities.PUser user = new UCenter.Entities.PUser();
            user.UName = name;
            user.Pass = pass;
            user.Pass2 = pass2;
            user.Email = email;
            user.School = school;
            user.ValidCode = vcode;

            //数据输入验证
            string msg;
            bool IsValid = DW.Framework.Validation.EntityValid.IsValid<DDM.UCenter.Entities.PUser>(user, out msg);
            MsgLabel.Text = msg;
            if (!IsValid)
            {
                MsgDiv.Visible = true;       
                return;
            }
     
    */
    /// </example>
    public class EntityValid
    {
        public static bool IsValid<T>(object o, out string msg)
        {
            ValidatorFactory valFactory = EnterpriseLibraryContainer.Current.GetInstance<ValidatorFactory>();

            Validator<T> userValid = valFactory.CreateValidator<T>();
            ValidationResults results = userValid.Validate(o);
            if (!results.IsValid)
            {
                StringBuilder builder = new StringBuilder();

                foreach (ValidationResult result in results)
                {
                    builder.AppendLine(string.Format("{0}", result.Message));
                    break;
                }

                msg = builder.ToString();
                return false;
            }
            msg = string.Empty;
            return true;
        }
    }

}
