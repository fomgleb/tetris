using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Requests;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class LoadProgressSystem : IEcsInitSystem
    {
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        private readonly EcsWorld _world = null;
        
        public void Init()
        {
            var progressData = Load<ProgressData>(_configuration.ProgressDataFileName);

            _runtimeData.HighScore = progressData.highScore;
            _world.NewEntity().Get<UpdateScoreViewRequest>();
        }
        
        private T Load<T>(string fileName)
        {
            var formatter = new BinaryFormatter();

            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fileStream.Length > 0 && formatter.Deserialize(fileStream) is T items)
                    return items;
                return default;
            }
        }
    }
}