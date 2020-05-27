﻿#if NETSTANDARD2_1
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TextCopy
{
    /// <inheritdoc />
    public class BlazorClipboard :
        IClipboard
    {
        static Type[] types =
        {
            typeof(string),
            typeof(CancellationToken),
            typeof(object[])
        };
        object jsRuntime;
        MethodInfo invokeAsync;

        /// <summary>
        /// Construct a new instance.
        /// </summary>
        public BlazorClipboard(object jsRuntime)
        {
            this.jsRuntime = jsRuntime;
            invokeAsync = jsRuntime.GetType()
                .GetMethod(
                    "InvokeAsync",
                    types: types);
            invokeAsync = invokeAsync.MakeGenericMethod(typeof(string));
        }

        /// <inheritdoc />
        public virtual async Task<string?> GetTextAsync(CancellationToken cancellation = default)
        {
            var parameters = new object[] {"navigator.clipboard.readText", cancellation, Array.Empty<object>()};
            return await (ValueTask<string>) invokeAsync.Invoke(jsRuntime, parameters);
        }

        /// <inheritdoc />
        public virtual string? GetText()
        {
            return GetTextAsync().GetAwaiter().GetResult();
        }

        /// <inheritdoc />
        public virtual async Task SetTextAsync(string text, CancellationToken cancellation = default)
        {
            var parameters = new object[] {"navigator.clipboard.writeText", cancellation, new object[] {text}};
            await (ValueTask<string>) invokeAsync.Invoke(jsRuntime, parameters);
        }

        /// <inheritdoc />
        public virtual void SetText(string text)
        {
            SetTextAsync(text).GetAwaiter().GetResult();
        }
    }
}
#endif