create proc DeleteAccount
@id nvarchar(128) as
delete from Query_Reply  where U_id = @id
delete from Query_Post  where U_id = @id
delete from Professional_Details  where U_id = @id
delete from UserDetails  where U_id = @id
delete from AspNetUsers  where Id = @id