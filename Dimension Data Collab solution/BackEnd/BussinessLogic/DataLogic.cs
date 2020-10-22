﻿using BackEnd.DataAccess;
using BackEnd.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BussinessLogic
{
    public static class DataLogic
    {
        //c
        public static void CreateDataItem(DataItem _item)
        {
            var DA = new DataItemAccess(SettingsHolder.CollectionName,SettingsHolder.DataBaseName);
            DA.InsertRecord(_item);
        }
        //r
        public async static Task<DataItem> GetItemById(ObjectId _id)
        {
            var DA = new DataItemAccess(SettingsHolder.CollectionName, SettingsHolder.DataBaseName);
            var item = await DA.GetRecordById(_id);
            return item;
        }
        public async static Task<List<DataItem>> GetItems(int page)
        {
            var DA = new DataItemAccess(SettingsHolder.CollectionName, SettingsHolder.DataBaseName);
            var items = await DA.GetAllRecords((page > 0 ? page : 1), SettingsHolder.PageSize);
            
            return items;
        }
        //u
        public static void UpdateItem(DataItem _item)
        {
            var DA = new DataItemAccess(SettingsHolder.CollectionName, SettingsHolder.DataBaseName);
            DA.UpsertRecord(_item._id,_item);
        }

        //d
        public static void DeleteItemById(ObjectId _id)
        {
            var DA = new DataItemAccess(SettingsHolder.CollectionName, SettingsHolder.DataBaseName);
            DA.DeleteRecord(_id);
        }

        public async static Task<long> GetCount()
        {
            var DA = new DataItemAccess(SettingsHolder.CollectionName, SettingsHolder.DataBaseName);
            return await DA.GetCount();
        }
    }
}
