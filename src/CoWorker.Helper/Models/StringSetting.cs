
namespace CoWorker.Helper.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// String with Action<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class StringSetting<T> : IEqualityComparer<StringSetting<T>>
    {
        public static StringSetting<T> Create(string text, Action<T> constructor) => new StringSetting<T>(text, constructor);

        public int GetHashCode(StringSetting<T> obj)
            => obj.GetHashCode();

        public override int GetHashCode()
            => GetHashCode(this);
        public override bool Equals(object obj)
            => Equals(this, obj as StringSetting<T>);

        public bool Equals(StringSetting<T> x, StringSetting<T> y)
            => x.Equals(y);

        private StringSetting(string text, Action<T> configure)
        {
            Text = text;
            Configure = configure;
        }

        public string Text { get; }
        public Action<T> Configure { get; }

        public static implicit operator string(StringSetting<T> strval) => strval.Text;
        public static implicit operator Action<T>(StringSetting<T> strval) => strval.Configure;
    }
}