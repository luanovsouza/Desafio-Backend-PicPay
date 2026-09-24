using DesafioBackEndPicpay.Enum;
using DesafioBackEndPicpay.Models;

namespace DesafioBackEndPicpay.Services.Interfaces;

public interface IUserService
{
    bool GetTypeUser(TypeUser typeUser);


    Task Transfer(decimal amount, User senderUser, User receiverUser);
}