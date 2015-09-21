﻿using SC.LoggingPortal.CastleWindsor;
using SC.LoggingPortal.Data.Entity;
using SC.LoggingPortal.Data.Repository;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Solr
{
    public class Indexer
    {
        private int updateCount = 1000;
        ISolrOperations<SolrLogMessage> solr;
        
        private LoggingRepository _loggingRepository;

        public Indexer()
        {
            solr = Windsor.Container.Resolve<ISolrOperations<SolrLogMessage>>();
            _loggingRepository = Windsor.Container.Resolve<LoggingRepository>();

            updateCount = int.Parse(ConfigurationManager.AppSettings["SolrIndexUpdate"]);
        }

        /// <summary>
        /// Reindexes the complete Solr index
        /// </summary>
        public async Task FullIndex()
        {
            solr.Delete(SolrQuery.All);

            List<SolrLogMessage> docs = new List<SolrLogMessage>();
            int count = 0;
            
            var all = await _loggingRepository.GetAllAsync();
            foreach (var x in all)
            {
                docs.Add(DMtoSolrModel(x));
                count++;

                if (count % updateCount == 0)
                {
                    solr.AddRange(docs);
                    docs.Clear();
                }
            }

            if(docs.Any())
            {
                solr.AddRange(docs);
                docs.Clear();
            }
            solr.Commit();
            solr.Optimize();
        }

        public void AddSingle(LogMessage message)
        {
            if (message != null)
            {
                solr.Add(DMtoSolrModel(message));
                solr.Commit();
            }
        }

        private SolrLogMessage DMtoSolrModel(LogMessage model)
        {
            return new SolrLogMessage()
            {
                Id = model.Id.ToString(),
                MessageId = model.Id,
                MachineName = model.MachineName,
                ApplicationName = model.ApplicationName,
                IPAddress = model.IPAddress,
                NetVersion = model.NetVersion,
                Is64BitProcess = model.Is64BitProcess,
                LoggerName = model.LoggerName,
                LogLevel = model.LogLevel,
                LoggerMessage = model.LoggerMessage,
                LogUserIdentity = model.LogUserIdentity,
                TimeStamp = model.TimeStamp
            };
        }

        public List<Data.Entity.LogMessage> getDummy()
        {
            return new List<LogMessage>()
            {
                new LogMessage()
                {
                    Id = Guid.NewGuid(),
                    ApplicationName = "test",
                    IPAddress = "192.168.1.1",
                    Is64BitProcess = true,
                    LoggerMessage = "SOLR Test",
                    TimeStamp = DateTime.Now,
                    LoggerName = "Rob",
                    LogLevel = "ERROR",
                    LogUserIdentity = "Rob Maas",
                    MachineName = "ESTA-LT04",
                    NetVersion = "4.0"
                },
                new LogMessage()
                {
                    Id = Guid.NewGuid(),
                    ApplicationName = "test",
                    IPAddress = "192.168.1.1",
                    Is64BitProcess = true,
                    LoggerMessage = "SOLR Test",
                    TimeStamp = DateTime.Now,
                    LoggerName = "Rob",
                    LogLevel = "ERROR",
                    LogUserIdentity = "Rob Maas",
                    MachineName = "ESTA-LT04",
                    NetVersion = "4.0"
                },
                new LogMessage()
                {
                    Id = Guid.NewGuid(),
                    ApplicationName = "test",
                    IPAddress = "192.168.1.1",
                    Is64BitProcess = true,
                    LoggerMessage = "SOLR Test",
                    TimeStamp = DateTime.Now,
                    LoggerName = "Rob",
                    LogLevel = "ERROR",
                    LogUserIdentity = "Rob Maas",
                    MachineName = "ESTA-LT04",
                    NetVersion = "4.0"
                },
                new LogMessage()
                {
                    Id = Guid.NewGuid(),
                    ApplicationName = "test",
                    IPAddress = "192.168.1.1",
                    Is64BitProcess = true,
                    LoggerMessage = "SOLR Test",
                    TimeStamp = DateTime.Now,
                    LoggerName = "Rob",
                    LogLevel = "ERROR",
                    LogUserIdentity = "Rob Maas",
                    MachineName = "ESTA-LT04",
                    NetVersion = "4.0"
                },
                new LogMessage()
                {
                    Id = Guid.NewGuid(),
                    ApplicationName = "test",
                    IPAddress = "192.168.1.1",
                    Is64BitProcess = true,
                    LoggerMessage = "SOLR Test",
                    TimeStamp = DateTime.Now,
                    LoggerName = "Rob",
                    LogLevel = "ERROR",
                    LogUserIdentity = "Rob Maas",
                    MachineName = "ESTA-LT04",
                    NetVersion = "4.0"
                }
            };
        }
    }
}