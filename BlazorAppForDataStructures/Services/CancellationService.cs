
public class CancellationService
{
    private CancellationTokenSource? _currentTokenSource;

    public CancellationToken GetToken()
    {
        if (_currentTokenSource == null || _currentTokenSource.IsCancellationRequested)
        {
            _currentTokenSource = new CancellationTokenSource();
        }

        return _currentTokenSource.Token;
    }

    public void CancelCurrentOperation()
    {
        _currentTokenSource?.Cancel();
    }

    public void ResetCancellationTokenSource()
    {
        CancelCurrentOperation();
        _currentTokenSource = new CancellationTokenSource();
    }
}


