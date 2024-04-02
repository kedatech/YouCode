using Microsoft.AspNetCore.Mvc;
using YouCode.BE;
using YouCode.BL;
using YouCode.GUI.Services;

namespace YouCode.GUI.Services
{
    public class PostService
    {

        public static async Task<bool> UpdateProfile(ProfileDto dto, string sesion_username)
        {
            UserBL userBL = new UserBL();
            ProfileBL profileBL = new ProfileBL();
            try{

                if(string.IsNullOrEmpty(sesion_username))
                {
                    return false;
                }

                var user  = await userBL.GetByUsernameAsync(sesion_username);
                var profile  = await profileBL.GetByIdAsync(new Profile{Id = user.Id});
                String avatarUrl = null;

                if(dto.AvatarFile != null)
                {
                    avatarUrl = await ImageService.SubirArchivo(dto.AvatarFile.OpenReadStream(), user.Username+"_profileImg_");
                    if(string.IsNullOrEmpty(avatarUrl))
                    {
                        return false;
                    }
                }

                user.Name = dto.Name ?? user.Name;
                profile.AvatarUrl =  avatarUrl ?? profile.AvatarUrl;
                profile.Bio = dto.Bio ?? profile.Bio;

                var Pres = await profileBL.UpdateAsync(profile);
                var Ures = await userBL.UpdateAsync(user);

                if(Pres == 0 || Ures == 0)
                {
                    return false;
                }

                return true;

            }catch{
                return false;
            }
        }

        public static async Task<ProfileDto> GetProfileDto(string sesion_username)
        {
            UserBL userBL = new UserBL();
            ProfileBL profileBL = new ProfileBL();
            try{

                if(string.IsNullOrEmpty(sesion_username))
                {
                    return new ProfileDto();
                }

                var user  = await userBL.GetByUsernameAsync(sesion_username);
                var profile  = await profileBL.GetByIdAsync(new Profile{Id = user.Id});
 

                var dto = new ProfileDto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Bio = profile.Bio
                };

                return dto;

            }catch{
                return new ProfileDto();
            }
        }

        public class ProfileDto
        {
            public int Id { get; set; }
            public IFormFile? AvatarFile { get; set; }
            public String? Name { get; set; }
            public String? Bio { get; set; }

        }
    
    }
}