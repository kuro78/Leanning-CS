using DependencyInversionSample.Common.String;
using System;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace DependencyInversionSample.Common
{
    /// <summary>
    /// String Resouce ��� Ŭ����
    /// </summary>
    public class DynamicResouce : DynamicObject
    {
        /// <summary>
        /// ��� ���� �̺�Ʈ
        /// </summary>
        public event EventHandler<string> LanguageChanged;
        /// <summary>
        /// ������ ���ҽ��δ�
        /// </summary>
        private readonly ResourceManager _resouceManager;
        protected CultureInfo CultureInfo = new("ko-KR");

        /// <summary>
        /// ������
        /// </summary>
        public DynamicResouce()
        {
            _resouceManager = new(typeof(CommonResource));
        }

        /// <summary>
        /// ������Ƽ�� ȣ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual string this[string id]
        {
            get
            {
                // 1. ���ҽ����� �� ��ȸ
                if (string.IsNullOrEmpty(id)) return null;
                string value = _resouceManager.GetString(id, CultureInfo);
                if(string.IsNullOrEmpty(value))
                {
                    // 2. ������ Ű ��ȯ
                    value = id;
                }

                return value;
            }
        }

        /// <summary>
        /// �̸����� ȣ��
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string id = binder.Name;
            string value = _resouceManager.GetString(id, CultureInfo);
            if (string.IsNullOrEmpty(value))
            {
                value = id;
            }

            result = value;
            return true;
        }

        public virtual void ChangeLanguage(string languageCode)
        {
            CultureInfo = new(languageCode);
            Thread.CurrentThread.CurrentCulture = CultureInfo;
            Thread.CurrentThread.CurrentUICulture = CultureInfo;
            // �������� ����ڵ� ����
            foreach(Window window in Application.Current.Windows.Cast<Window>())
            {
                if(!window.AllowsTransparency)
                {
                    window.Language = XmlLanguage.GetLanguage(CultureInfo.Name);
                }
            }

            if(LanguageChanged != null)
            {
                LanguageChanged.Invoke(this, CultureInfo.Name);
            }
        }
    }
}
