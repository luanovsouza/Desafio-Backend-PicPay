using DesafioBackEndPicpay.Dtos;
using DesafioBackEndPicpay.Enum;
using DesafioBackEndPicpay.Models;
using DesafioBackEndPicpay.Services.Interfaces;

namespace DesafioBackEndPicpay.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;

    public UserService(AppDbContext appDbContext, IHttpClientFactory httpClientFactory)
    {
        _context = appDbContext;
        _httpClient = httpClientFactory.CreateClient();
    }

    public bool GetTypeUser(TypeUser typeUser)
    {
        return typeUser == TypeUser.Shop;
    }

    public async Task<bool?> GetApiTransfer()
    {   
        //Acesa o Mock
        string url = "https://util.devi.tools/api/v2/authorize";
        
        //Pega o conteudo
        var response =  await _httpClient.GetFromJsonAsync<ApiTransferAuthorize>(url);
        
        //Retorna a propriedade de autorização do Api
        return response!.Data.Authorization;
    }
    
    public async Task Transfer(decimal amount, User senderUser,  User receiverUser)
    {
        //Comando para voltar tudo se caso nao funcionar
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        if (GetTypeUser(senderUser.TypeUser))
            throw new Exception("Merchants cannot make transfers!");
        
        if (senderUser.Balance < amount)
            throw new Exception("You do not have a balance, for this transfer!");

        try
        {
            var authorization = await GetApiTransfer();
            
            if(authorization == null)
                throw new Exception("Transfer not authrorized!!");
            
            
            senderUser.Balance -= amount;
            receiverUser.Balance += amount;

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            //Volta tudo se der ruim
            await transaction.RollbackAsync();
            throw;
        }
    }
}