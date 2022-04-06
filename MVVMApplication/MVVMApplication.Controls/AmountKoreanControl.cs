using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVMApplication.Controls
{
    [TemplatePart(Name = _textBlockName, Type = typeof(TextBlock))]
    [TemplatePart(Name = _textBoxName, Type = typeof(TextBox))]
    public class AmountKoreanControl : Control
    {
        /// <summary>
        /// AmountKoreanControl 컨트롤에서 제어할 컨트롤들의 이름
        /// </summary>
        private const string _textBlockName = "PART_KoreanDisplay";
        private const string _textBoxName = "PART_Amount";

        /// <summary>
        /// AmountKoreanControl 컨트롤에서 제어할 컨트롤
        /// </summary>
        private TextBlock _koreanDisplayTextBlock;
        private TextBox _amountTextBox;

        private bool _isWork;

        static AmountKoreanControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AmountKoreanControl), new FrameworkPropertyMetadata(typeof(AmountKoreanControl)));
        }

        /// <summary>
        /// 커스텀 컨트롤에 텡플릿이 적용될 때
        /// </summary>
        public override void OnApplyTemplate()
        {
            // 커스텀 컨트롤 각 PART 를 내부에서 사용할 수 있도록 가져옴
            _koreanDisplayTextBlock = GetTemplateChild(_textBlockName) as TextBlock;
            _amountTextBox = GetTemplateChild(_textBoxName) as TextBox;

            if (_amountTextBox == null
                || _koreanDisplayTextBlock == null)
            {
                throw new NullReferenceException("Can not found part of control.");
            }

            //Amount 초기값을 설정
            DecimalToFormatString(Amount);

            _amountTextBox.TextChanged += AmountTextBox_TextChanged;
            _amountTextBox.PreviewKeyDown += AmountTextBox_PreviewKeyDown;
        }

        /// <summary>
        /// 키다운 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmountTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool isDigit = false;
            switch ((int)e.Key)
            {
                case int n when ((34 <= n && 43 >= n) || (74 <= n && 83 >= n)):
                    isDigit = true;
                    break;
            }

            if (!(isDigit || e.Key == Key.Back
                || e.Key == Key.Left || e.Key == Key.Right))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 텍스트 체인지 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // 중복실행방지
            if (_isWork)
            {
                return;
            }
            _isWork = true;

            // 전처리
            var numberTextOnly = _amountTextBox.Text.Trim().Replace(",", "");
            if (decimal.TryParse(numberTextOnly, out decimal decimalValue))
            {
                DecimalToFormatString(decimalValue);
            }
            else
            {
                Amount = 0;
                _koreanDisplayTextBlock.Text = "";
            }
            _isWork = false;
        }

        /// <summary>
        /// 데시멀을 포맷스트링으로 입력과 한글 금액 출력
        /// </summary>
        /// <param name="decimalValue"></param>
        private void DecimalToFormatString(decimal decimalValue)
        {
            // DP 변경 이벤트로 호출되는 경우 초기화 전에 들어오는 것 방지
            if (_amountTextBox == null)
            {
                return;
            }
            // StringFormat 출력
            _amountTextBox.Text = string.Format("{0:#,##0}", decimalValue);
            // 캐럿을 맨뒤로
            _amountTextBox.SelectionStart = _amountTextBox.Text.Length;

            // DP에 값 입력
            Amount = decimalValue;
            var korean = Number2Hangle(Amount);
            _koreanDisplayTextBlock.Text = korean;
        }

        /// <summary>
        /// 컨트롤 종료자
        /// </summary>
        ~AmountKoreanControl()
        {
            _amountTextBox.TextChanged -= AmountTextBox_TextChanged;
            _amountTextBox.PreviewKeyDown -= AmountTextBox_PreviewKeyDown;
        }

        /// <summary>
        /// 숫자를 한글로 - 이 부분은 따로 Utility로 떼서 사용하는 것이 더 좋을듯
        /// </summary>
        /// <param name="lngNumber"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://redqueen-textcube.blogspot.com/2009/12/c-%EC%88%AB%EC%9E%90%EA%B8%88%EC%95%A1-%EB%B3%80%ED%99%98.html
        /// </remarks>
        private string Number2Hangle(decimal lngNumber)
        {
            string sign = "";
            string[] numberChar = new string[] { "", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" };
            string[] levelChar = new string[] { "", "십", "백", "천" };
            string[] decimalChar = new string[] { "", "만", "억", "조", "경" };

            string strValue = string.Format("{0}", lngNumber);
            string numToKorea = sign;
            bool useDecimal = false;

            for (int i = 0; i < strValue.Length; i++)
            {
                int level = strValue.Length - i;
                if (strValue.Substring(i, 1) != "0")
                {
                    useDecimal = true;
                    if (((level - 1) % 4) == 0)
                    {
                        numToKorea += numberChar[int.Parse(strValue.Substring(i, 1))] + decimalChar[(level - 1) / 4];
                        useDecimal = false;
                    }
                    else
                    {
                        if (strValue.Substring(i, 1) == "1")
                        {
                            numToKorea += levelChar[(level - 1) % 4];
                        }
                        else
                        {
                            numToKorea += numberChar[int.Parse(strValue.Substring(i, 1))] + levelChar[(level - 1) % 4];
                        }
                    }
                }
                else
                {
                    if ((level % 4 == 0) && useDecimal)
                    {
                        numToKorea += decimalChar[level / 4];
                        useDecimal = false;
                    }
                }
            }

            return numToKorea;
        }


        public decimal Amount
        {
            get { return (decimal)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        /// <summary>
        /// Amount DP
        /// </summary>
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(nameof(Amount), typeof(decimal), typeof(AmountKoreanControl), new PropertyMetadata(decimal.Zero, AmountChanged));

        private static void AmountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (AmountKoreanControl)d;
            control.SetAmount();
        }

        private void SetAmount()
        {
            if (_isWork)
            {
                return;
            }
            _isWork = true;
            DecimalToFormatString(Amount);
            _isWork = false;
        }
    }
}
