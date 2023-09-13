#if NET6_0

namespace TextCopy;

/// <inheritdoc />
public class BlazorClipboard :
    IClipboard
{
    static Type[] types =
    {
        typeof(string),
        typeof(object[])
    };
    object jsRuntime;
    MethodInfo invokeAsync;

    // System.Threading.Tasks.ValueTask`1[TValue] InvokeAsync[TValue](System.String, System.Object[])
    /// <summary>
    /// Construct a new instance.
    /// </summary>
    public BlazorClipboard(object jsRuntime)
    {
        this.jsRuntime = jsRuntime;
        var type = jsRuntime.GetType();
        var method = type.GetMethod("InvokeAsync", types);
        if (method == null)
        {
            var methodNames = type.GetMethods().Select(_ => _.ToString());
            throw new($"Unable to find InvokeAsync on {type.FullName}. Methods:{Environment.NewLine}{string.Join(Environment.NewLine, methodNames)}");
        }

        invokeAsync = method.MakeGenericMethod(typeof(string));
    }

    /// <inheritdoc />
    public virtual async Task<string?> GetTextAsync(Cancellation cancellation = default)
    {
        var parameters = new object[] {"navigator.clipboard.readText", Array.Empty<object>()};
        return await (ValueTask<string>) invokeAsync.Invoke(jsRuntime, parameters)!;
    }

    /// <inheritdoc />
    public virtual string? GetText()
    {
        return GetTextAsync().GetAwaiter().GetResult();
    }

    /// <inheritdoc />
    public virtual async Task SetTextAsync(string text, Cancellation cancellation = default)
    {
        var parameters = new object[] {"navigator.clipboard.writeText", new object[] {text}};
        await (ValueTask<string>) invokeAsync.Invoke(jsRuntime, parameters)!;
    }

    /// <inheritdoc />
    public virtual void SetText(string text)
    {
        SetTextAsync(text).GetAwaiter().GetResult();
    }
}
#endif