﻿using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddressBookRL
    {
        IEnumerable<AddressBookEntity> GetAllContacts();
        AddressBookEntity GetContactById(int id);
        AddressBookEntity AddContact(AddressBookEntity contact);
        AddressBookEntity UpdateContact(int id, AddressBookEntity contact);
        bool DeleteContact(int id);
    }
}