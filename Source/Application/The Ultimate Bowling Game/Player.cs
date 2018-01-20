using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Player
    {
        private List<Frame> _frames;

        public string Name { get; internal set; }
        public int ID { get; internal set; }

        public Player(){
            _frames = Get10Frames();
        }

        private List<Frame> Get10Frames()
        {
            var list = new List<Frame>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(new Frame { Id = i });

            }

            return list;
        }

        public int Score() {

            var score = 0;

            foreach (var frameDetail in _frames.Select((key,index) => new { frame = key, index })) {
                var currentFrame = frameDetail.frame;
                var nextFrame = _frames.Count >= frameDetail.index + 2 ? _frames[frameDetail.index + 1] : null ;
                var twoFramesAway = nextFrame != null && _frames.Count >= frameDetail.index + 3 ? _frames[frameDetail.index + 2] : null;

                if (currentFrame.Throw2 != null && currentFrame.Throw2.Score == "/")
                {
                    score += 10;
                    if (nextFrame != null && nextFrame.Throw1.Score == "x")
                    {
                        score += 10;
                    }
                    else if (nextFrame != null)
                    {
                        score += nextFrame.Throw1.ScoreAsInt;
                    }
                    else if (currentFrame.Throw2.Score != null)
                    {
                        score += currentFrame.Throw1.ScoreAsInt + currentFrame.Throw2.ScoreAsInt;
                    }
                }
                else if (currentFrame.Throw1 != null && currentFrame.Throw1.Score.ToLower() == "x")
                {
                    score += 10;

                    if (nextFrame != null && nextFrame.Throw1.Score.ToLower() == "x")
                    {
                        score += 10;

                        if (twoFramesAway != null && twoFramesAway.Throw1.Score.ToLower() == "x")
                        {
                            score += 10;
                            continue;
                        }
                        else if(twoFramesAway != null)
                        {
                            score += twoFramesAway.Throw1.ScoreAsInt;
                            continue;
                        }
                    }
                    else if (currentFrame.Throw2 != null && currentFrame.Throw2.Score == "/")
                    {
                        score += 10;
                        if (nextFrame != null && nextFrame.Throw1.Score == "x")
                        {
                            score += 10;
                        }
                        else if (nextFrame != null)
                        {
                            score += nextFrame.Throw1.ScoreAsInt;
                        }
                    }
                    else if (nextFrame != null) {
                        score += nextFrame.Throw1.ScoreAsInt + nextFrame.Throw2.ScoreAsInt;
                        continue;
                    }

                }
                else
                {
                    score += currentFrame.Throw1.ScoreAsInt + currentFrame.Throw2.ScoreAsInt;
                    continue;
                }

            }

            return score;        }

        internal void AddThrow(int frameId, int throwNumber, string throwScore) {
            var frame = _frames.Where(x => x.Id == frameId).FirstOrDefault();
            if (frame == null)
            {
                return;
            }
            if (throwNumber == 1)
            {
                    frame.Throw1 = new Throw { Score = throwScore };
            }
            if (throwNumber == 2)
            {
                frame.Throw2 = new Throw { Score = throwScore };
            }

        }
    }
}
