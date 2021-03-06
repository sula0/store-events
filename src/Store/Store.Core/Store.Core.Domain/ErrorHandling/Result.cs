using System;
using System.Threading.Tasks;

namespace Store.Core.Domain.ErrorHandling;

public class Result
{
    private static Result OkValue = new();

    private protected readonly Error _error;

    public bool IsOk { get; }
    public bool IsError => !IsOk;

    private protected Result() => IsOk = true;

    private protected Result(Error error)
    {
        Ensure.NotNull(error);
        
        _error = error;
        IsOk = false;
    }

    public void Match(Action ok, Action<Error> error)
    {
        if (IsOk)
        {
            ok();
        }
        else
        {
            error(_error);
        }
    }

    public Task Match(Func<Task> ok, Func<Error, Task> error)
        => IsOk ? ok() : error(_error);

    public TResult Match<TResult>(Func<TResult> ok, Func<Error, TResult> error)
        => IsOk ? ok() : error(_error);

    public Task<TResult> Match<TResult>(Func<Task<TResult>> ok, Func<Error, Task<TResult>> error)
        => IsOk ? ok() : error(_error);
    
    public Result<TResult> Then<TResult>(Func<Result<TResult>> ok)
        => IsOk ? ok() : Error<TResult>(_error);
    
    public async Task<Result<TResult>> Then<TResult>(Func<Task<Result<TResult>>> ok)
        => IsOk ? await ok() : Error<TResult>(_error);

    public async Task<Result<TResult>> Then<TResult>(Func<Task<TResult>> ok)
        => IsOk ? Ok(await ok()) : Error<TResult>(_error);
    
    public async Task<Result> Then(Func<Task<Result>> ok)
        => IsOk ? await ok() : _error;

    public static Result<T> Ok<T>(T value)
    {
        Ensure.NotNull(value);
        return new(value);
    }

    public static Result Ok() => OkValue;

    public static Result Error(Error error) => new(error);

    public static Result<T> Error<T>(Error error)
    {
        Ensure.NotNull(error);
        return new(error);
    }

    public static implicit operator Result(Error error) => new(error);
}

public sealed class Result<T> : Result
{
    private readonly T _value;

    internal Result(T value)
    {
        Ensure.NotNull(value);
        _value = value;
    }

    internal Result(Error error) : base(error)
    {
    }

    public bool TryGetValue(out T value)
    {
        if (IsError)
        {
            value = default;
            return false;
        }

        value = _value;
        return true;
    }

    public T UnwrapOrDefault() => UnwrapOrDefault(default);

    public T UnwrapOrDefault(T defaultValue) => !TryGetValue(out T value) ? defaultValue : value;

    public T Unwrap() => Expect("Cannot unwrap Option<T> without value.");

    public T Expect(string message)
    {
        if (IsError) throw new InvalidOperationException(message);
        return _value;
    }

    public void Match(Action<T> ok, Action<Error> error)
    {
        if (IsOk)
        {
            ok(_value);
        }
        else
        {
            error(_error);
        }
    }

    public Task Match(Func<T, Task<T>> ok, Func<Error, Task> error)
        => IsOk ? ok(_value) : error(_error);

    public TResult Match<TResult>(Func<T, TResult> ok, Func<Error, TResult> error)
        => IsOk ? ok(_value) : error(_error);

    public Task<TResult> Match<TResult>(Func<T, Task<TResult>> ok, Func<Error, Task<TResult>> errors)
        => IsOk ? ok(_value) : errors(_error);
    
    #region T

    public Result<TResult> Then<TResult>(Func<T, TResult> ok)
        => IsOk ? Ok(ok(_value)) : Error<TResult>(_error);

    public async Task<Result<TResult>> Then<TResult>(Func<T, Task<TResult>> ok)
        => IsOk ? Ok(await ok(_value)) : Error<TResult>(_error);
    
    #endregion

    #region PlainResult
    
    public Result Then(Func<T, Result> ok)
        => IsOk ? ok(_value) : Error(_error);
    
    public async Task<Result> Then(Func<T, Task<Result>> ok)
        => IsOk ? await ok(_value) : Error(_error);
    
    #endregion
    
    #region Result<T>
    
    public Result<TR> Then<TR>(Func<T, Result<TR>> ok)
        => IsOk ? ok(_value) : Error<TR>(_error); 
    
    public async Task<Result<TR>> Then<TR>(Func<T, Task<Result<TR>>> ok)
        => IsOk ? await ok(_value) : Error<TR>(_error); 
    
    #endregion

    public static implicit operator Result<T>(Error error) => new(error);
    public static implicit operator Result<T>(T value) => new(value);
}