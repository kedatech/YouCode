// Inicializar cuando el contenido esté cargado completamente
document.addEventListener('DOMContentLoaded', function () {
    followInit();
});

function followInit() {
    // obtener id del usuario
    let stringId = localStorage.getItem('YCuserId');
    let userId = parseInt(stringId);

    
    // recorrer las sugerencias de follow user
    let follows = document.querySelectorAll('.who-follow-render');

    // si no se encuentar el id del usuario, en lugar de agregar evento click se agrega un alert al intentar seguir
    if (!userId) {
        follows.forEach(follow => {
            let followButton = follow.querySelector('button');
            followButton.addEventListener('click', function () {
                alert('Debes iniciar sesión para seguir a un usuario');
            });
        });
        return;
    }
    
    // recorrer cada follow y verificar que el usuario no se siga a si mismo
    follows.forEach(follow => {
        let followId = parseInt(follow.getAttribute('itemid'));
        // si es el mismo quitar
        if (followId === userId) {
            follow.remove();
        }

        let followButton = follow.querySelector('button');
        // agregar evento click al boton
        followButton.addEventListener('click', function () {
            setFollow(userId, followId);
        });
    });


    // hacer peticion para obtener a los seguidos
    let url = `http://localhost:5096/api/follower/${userId}`;
    fetch(url)
        .then(response => response.json())
        .then(data => {
            console.log(data)
            // recorrer los seguidos
            data.forEach(follow => {
                // cambiar texto del boton
                let followButton = document.querySelector(`.who-follow-render[itemid="${follow.idFollow}"] button`);
                console.log(followButton);
                if (!followButton) {
                    return;
                }
                followButton.textContent = 'Unfollow';
                // cambiar color background
                followButton.classList.add('bg-gray-500');
            });
        })

}

function setFollow(userId, followId) {
    let followButton = document.querySelector(`.who-follow-render[itemid="${followId}"] button`);
    if (followButton.textContent === 'Follow') {
        followUser(userId, followId);
    } else {
        unfollowUser(userId, followId);
    }
}


function unfollowUser(userId, followId) {
    console.log('unfollow', userId, followId);
    // no tiene body

    // enviar al servidor
    // [HttpDelete("{followerId}/{followId}")]
    fetch(`/api/follower/${userId}/${followId}`, {
        method: 'DELETE'
    })
        .then(response => {
            if (response.ok) {
                // cambiar texto del boton
                let followButton = document.querySelector(`.who-follow-render[itemid="${followId}"] button`);
                followButton.textContent = 'Follow';
                // cambiar color background
                followButton.classList.remove('bg-gray-500');
            } else {
                alert('Error al dejar de seguir usuario');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function followUser(userId, followId) {
    console.log('follow', userId, followId);
    // crear objeto
    let follower = {
        IdFollower: userId,
        IdFollow: followId
    };

    // enviar al servidor
    fetch('/api/follower', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(follower)
    })
        .then(response => {
            if (response.ok) {
                // cambiar texto del boton
                let followButton = document.querySelector(`.who-follow-render[itemid="${followId}"] button`);
                followButton.textContent = 'Unfollow';
                // cambiar color background
                followButton.classList.add('bg-gray-500');
            } else {
                alert('Error al seguir usuario');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

{/* <div class="mt-4 flex flex-wrap">
@foreach (var user in ViewBag.UsersToFollow)
{
    <div 
        class="bg-white rounded-md p-2 my-2 w-full grid grid-cols-10 who-follow-render"
        itemid="@user.Id"
    >
        <div class="col-span-2">
            <div class="aspect-w-1 aspect-h-1">
                <img src="https://picsum.photos/400" alt="User 1"
                    class="object-cover w-full h-full p-2 rounded-full">
            </div>
        </div>
        <div class="col-span-5 flex flex-col w-full justify-center">
            <h3 class="font-semibold text-gray-800 text-sm">@user.Name</h3>
            <h3 class="font-semibold text-gray-500 text-sm"> @user.Username</h3>
        </div>

        <div class="col-span-3 px-2 py-4 flex justify-center items-center">
            <button
                class=" bg-[#45a448] max-h-5 hover:bg-[#5bc25f] text-white text-[13px] font-bold px-2 rounded">Follow</button>
        </div>
    </div>
}


</div> */}

// controller
// using Microsoft.AspNetCore.Mvc;
// using YouCode.BE;
// using YouCode.BL;
// using System.Threading.Tasks;

// namespace YouCode.GUI.Controllers.Api
// {
//     [ApiController]
//     [Route("api/follower")]
//     public class FollowerController : ControllerBase
//     {
//         private readonly FollowerBL _followerBL = new FollowerBL();

//         [HttpGet]
//         [Route("{id}")]
//         public async Task<IActionResult> GetAll(int id)
//         {
//             var followers = await _followerBL.GetAllAsync();
//             var filteredFollowers = followers.FindAll(f => f.IdFollower == id);
//             return Ok(filteredFollowers);
//         }

//         [HttpPost]
//         public async Task<IActionResult> Create([FromBody] Follower follower)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             int result = await _followerBL.CreateAsync(follower);
//             return CreatedAtAction("Get", new { id = follower.Id }, follower);
//         }


//         [HttpDelete("{id}")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             var result = await _followerBL.DeleteAsync(new Follower { Id = id });
//             return NoContent();
//         }
//     }
// }
