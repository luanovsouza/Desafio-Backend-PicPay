namespace DesafioBackEndPicpay.Dtos;

public class ApiTransferAuthorize
{
    public string Status { get; set; }
    
    public AuthorizationData Data { get; set; }
}


public class AuthorizationData
{
    public bool Authorization { get; set; }
}