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
            if (_jsRuntime == null)
            {
                throw new InvalidOperationException("JavaScript interop is not available.");
            }

            var encryptedValue = EncryptionHelper.Encrypt(value, _encryptionKey);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, encryptedValue);
        }

        public async Task<string> GetAsync(string key)
        {
            try
            {
                // Call the JavaScript method to get the item from localStorage
                var encryptedValue = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

                // Decrypt the value if it's not null
                return encryptedValue != null ? EncryptionHelper.Decrypt(encryptedValue, _encryptionKey) : string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting item from localStorage: {ex.Message}");
                return string.Empty; // Return an empty string if there's an error
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                // Call the JavaScript method to remove the item from localStorage
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing item from localStorage: {ex.Message}");
            }
        }
    }
}