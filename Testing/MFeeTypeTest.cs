
using ManyEvents.Controllers;
using Xunit;
using System;
using ManyEvents.Models;

namespace Testing;

public class MFeeTypeTest : IDisposable
{
    MFeeType testCase;

    public MFeeTypeTest()
    {
        testCase = new MFeeType();
        testCase.SetName("sularaha");

    }

    
    public void Dispose()
    {
        // close down

        // close the DB
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal("sularaha", testCase.Name);
    }

}
