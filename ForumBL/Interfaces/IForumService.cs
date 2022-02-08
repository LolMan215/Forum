using ForumBL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumBL.Interfaces
{
    public interface IForumService: ICrud<ForumDTO>
    {
        Task UpdateAsync(int id, ForumDTO model);

        Task<List<ForumDTO>> GetAllTopLevels();

        List<ForumDTO> Get();
    }
}
