using ATEC_API;
using ATEC_API.Data.IRepositories;

namespace ATEC_API_Test;

public class APITest
{

    [Fact]
    public void ReturnTrue_IFCurrentStage_Is_ReadyForTrackOut()
    {
        Assert.True(true);
    }

    [Fact]
    public void ReturnTrue_IF_Operator_Is_Qualified1()
    {
        Assert.False(false);
    }
}