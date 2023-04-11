
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
        testCase.SetRemarks("EUR");

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

    [Fact]
    public void Test2()
    {
        Assert.Equal("EUR", testCase.Remarks);
    }


}
