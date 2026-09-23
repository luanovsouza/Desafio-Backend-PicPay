using DesafioBackEndPicpay.Dtos;
using DesafioBackEndPicpay.Enum;
using DesafioBackEndPicpay.Models;
using DesafioBackEndPicpay.Repositories.Interfaces;

namespace DesafioBackEndPicpay.Services.Interfaces;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;

    protected UserService(AppDbContext appDbContext, HttpClient httpClient)
    {
        _context = appDbContext;
        _httpClient = httpClient;
    }

    public bool GetTypeUser(TypeUser typeUser)
    {
        return typeUser == TypeUser.Shop;
    }

    protected async Task<bool?> GetApiTransfer()
    {   
        //Acesa o Mock
        string url = "https://util.devi.tools/api/v2/authorize";
        
        //Pega o conteudo
        var response =  await _httpClient.GetFromJsonAsync<ApiTransferAuthorize>(url);
        
        //Retorna a propriedade de autorização do Api
        return response.Data.Authorization;
    }
    
    public async Task Transfer(decimal amount, User senderUser,  User receiverUser)
    {
        if (GetTypeUser(senderUser.TypeUser))
            throw new Exception("Merchants cannot make transfers!");
        
        if (senderUser.Balance < amount)
            throw new Exception("You do not have a balance, for this transfer!");

        try
        {
            var authorization = await GetApiTransfer();
            
            if(authorization != null)
                throw new Exception("Transfer not authrorized!!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        
        
        senderUser.Balance -= amount;
        receiverUser.Balance += amount;

        await _context.SaveChangesAsync();
    }
}