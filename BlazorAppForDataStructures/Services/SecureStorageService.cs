using Microsoft.JSInterop;
namespace BlazorAppForDataStructures.Services

{
    public class SecureStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public SecureStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        // Save a key-value pair in local storage
        public async Task SetAsync(string key, string value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }

        // Retrieve a value from local storage
        public async Task<string?> GetAsync(string key)
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
        }

        // Remove a key-value pair from local storage
        public async Task RemoveAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}