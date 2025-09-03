
// public static class EntityMapper
// {
//     public static Entity ToEntity(this EntityDto dto)
//     {
//         Entity address = new()
//         {
//             Id = dto.Id,
//             Deleted = dto.Deleted,
//             Registered = dto.Registered,
            

//         };

//         return address;
//     }

//     public static EntityDto ToDto(this Entity entity)
//     {
//         EntityDto address = new()
//         {
//             Id = entity.Id,
//             Deleted = entity.Deleted,
//             Registered = entity.Registered,
            
//         };
//         return address;
//     }

//     public static EntityDto Incomplete()
//     {
//         EntityDto incomplete = new()
//         {
//             Id = 0,
//             Deleted = DateTime.MinValue,
//             Registered = DateTime.Now,
            
//         };

//         return incomplete;
//     }


// }