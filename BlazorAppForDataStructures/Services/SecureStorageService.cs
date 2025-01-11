using BlazorAppForDataStructures.Helpers;
using Microsoft.JSInterop;

namespace BlazorAppForDataStructures.Services
{
    public class SecureStorageService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly string _encryptionKey;

        public SecureStorageService(IJSRuntime jsRuntime, IConfiguration configuration)
        {
            _jsRuntime = jsRuntime;
            _encryptionKey = configuration["SuperSecretKey"]
                ?? throw new InvalidOperationException("Encryption key is not configured.");
        }

        public async Task SetAsync(string key, string value)
        {
            var encryptedValue = EncryptionHelper.Encrypt(value, _encryptionKey);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, encryptedValue);
        }

        public async Task<string> GetAsync(string key)
        {
            var encryptedValue = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            return encryptedValue != null ? EncryptionHelper.Decrypt(encryptedValue, _encryptionKey) : string.Empty;
        }

        public async Task RemoveAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
}