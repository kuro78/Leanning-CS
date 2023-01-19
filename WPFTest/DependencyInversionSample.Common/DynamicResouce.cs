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
    /// String Resouce 사용 클래스
    /// </summary>
    public class DynamicResouce : DynamicObject
    {
        /// <summary>
        /// 언어 변경 이벤트
        /// </summary>
        public event EventHandler<string> LanguageChanged;
        /// <summary>
        /// 윈도우 리소스로더
        /// </summary>
        private readonly ResourceManager _resouceManager;
        protected CultureInfo CultureInfo = new("ko-KR");

        /// <summary>
        /// 생성자
        /// </summary>
        public DynamicResouce()
        {
            _resouceManager = new(typeof(CommonResource));
        }

        /// <summary>
        /// 프로퍼티로 호출
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual string this[string id]
        {
            get
            {
                // 1. 리소스에서 값 조회
                if (string.IsNullOrEmpty(id)) return null;
                string value = _resouceManager.GetString(id, CultureInfo);
                if(string.IsNullOrEmpty(value))
                {
                    // 2. 없으면 키 반환
                    value = id;
                }

                return value;
            }
        }

        /// <summary>
        /// 이름으로 호출
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
            // 윈도우의 언어코드 변경
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
