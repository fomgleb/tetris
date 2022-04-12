using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Requests;

namespace Tetris.Scripts.Systems
{
    public class SaveProgressSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SaveProgressRequest> _saveProgressRequests = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;


        public void Run()
        {
            foreach (var entityIndex in _saveProgressRequests)
            {
                var progressData = new ProgressData
                {
                    highScore = _runtimeData.HighScore 
                };

                Save(_configuration.ProgressDataFileName, progressData);
            }
        }
        
        private void Save(string fileName, object item)
        {
            var formatter = new BinaryFormatter();

            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fileStream, item);
            }
        }
    }

    [Serializable]
    public struct ProgressData
    {
        public int highScore;
    }
}