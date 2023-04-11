
using ManyEvents.Controllers;
using Xunit;
using System;
using ManyEvents.Models;

namespace Testing;

public class MCompanyTest : IDisposable
{
    MCompany testCase;

    public MCompanyTest()
    {
        testCase = new MCompany();
        testCase.SetJurName("ABB");
        testCase.SetReqCode("75666");
        testCase.SetGuestsCount(10);

    }

    
    public void Dispose()
    {
        // close down

        // close the DB
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal("ABB", testCase.JurName);
    }

    


}
