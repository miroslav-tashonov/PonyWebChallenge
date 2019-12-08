using PonyWebChallenge.Helper;
using PonyWebChallenge.Models;
using System;
using System.Runtime.Serialization;
using static PonyWebChallenge.Models.CellStateEnumModel;

namespace PonyWebChallenge.MazeGeneration
{
    [DataContract]
    public class Maze
    {
        private Guid _mazeId;

        private int _DomokunId;
        private int _ponyId;
        private int _endpointId;

        private readonly int _difficulty;
        private readonly string _playerName;
        private readonly CellState[] _cells;
        private readonly int _width;
        private readonly int _height;
        private readonly Random _rng;


        public Maze(MazeInitModel model)
        {
            _playerName = model.PlayerName;
            _difficulty = (int)model.Difficulty;

            _width = model.MazeWidth;
            _height = model.MazeHeight;
            _cells = new CellState[_width * _height];
            for (var x = 0; x < _height; x++)
                for (var y = 0; y < _width; y++)
                    _cells[(x * _width) + y] = CellState.Initial;
            _rng = new Random();

            MazeHelper.VisitCell(this, _rng.Next(_width * _height), _rng);
        }

        #region Get and Set Methods
        public CellState this[int xy]
        {
            get { return _cells[xy]; }
            set { _cells[xy] = value; }
        }

        public int GetDifficulty()
        {
            return this._difficulty;
        }

        public Guid GetMazeId()
        {
            return this._mazeId;
        }

        public void SetMazeId(Guid mazeId)
        {
            this._mazeId = mazeId;
        }

        public int GetMazeArea()
        {
            return this._height * this._width;
        }

        public string GetPlayerName()
        {
            return this._playerName;
        }

        public void SetCharacterId(CellState character, int id)
        {
            if (character == CellState.Pony)
                SetPonyId(id);
            else
                SetDomokunId(id);
        }

        public void SetDomokunId(int id)
        {
            this._DomokunId = id;
            this[id] |= CellState.Domokun;
        }

        public void SetPonyId(int id)
        {
            this._ponyId = id;
            this[id] |= CellState.Pony;
        }

        public void SetEndpointId(int id)
        {
            this._endpointId = id;
            this[id] |= CellState.Endpoint;
        }

        public int GetDomokunId()
        {
            return this._DomokunId;
        }

        public int GetPonyId()
        {
            return this._ponyId;
        }

        public int GetEndpointId()
        {
            return this._endpointId;
        }

        public int GetMazeHeight()
        {
            return this._height;
        }

        public int GetMazeWidth()
        {
            return this._width;
        }

        public CellState[] GetCells()
        {
            return this._cells;
        }
        #endregion
    }
}