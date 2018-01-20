namespace ConsoleApp1
{
    public class Frame
    {
        public int Id { get; internal set; }
        
        public Throw Throw1 { get; internal set; }
        public Throw Throw2 { get; internal set; }
        public Throw Throw3 { get; internal set; }
        public int? Score(Frame nextFrame = null, Frame twoFramesAway = null) {
            if (Throw1 == null) return Throw1.ScoreAsInt;
            if (Throw2 == null) return Throw2.ScoreAsInt;
            var score = 0;

            if (Throw1.Score.ToLower() == "x")
            {
                //    if (current frame throw1 == X)
                //then
                //    score = 10 + add next two throws(regardless of the frame)

                score = 10;
                if (nextFrame != null && nextFrame.Throw1 != null)
                {

                    if (nextFrame.Throw1.Score.ToLower() == "x")
                    {
                        score += 10;

                        if (twoFramesAway != null && twoFramesAway.Throw1 != null)
                        {
                            var throw1Score = 0;
                            int.TryParse(twoFramesAway.Throw1.Score, out throw1Score);

                            score += twoFramesAway.Throw1.Score.ToLower() == "x" ? 10 : throw1Score;
                        }
                    }
                    else
                    {
                        var throw1Score = 0;
                        if (int.TryParse(nextFrame.Throw1.Score, out throw1Score) && nextFrame.Throw2.Score != "/")
                        {
                            score += throw1Score;

                            var throw2Score = 0;
                            if (nextFrame.Throw2 != null && int.TryParse(nextFrame.Throw2.Score, out throw2Score))
                            {
                                score += throw2Score;
                            }

                        }
                        else if (nextFrame.Throw2.Score == "/")
                            score += 10;
                    }
                }
            }
            else if (Throw2 != null && Throw2.Score == "/")
            {
                score += 10;
                //    if else (current frame throw1 != X && throw2 = /)
                //then
                //    score = 10 + add next throw
            }
            else {
                score = Throw1.ScoreAsInt + Throw2.ScoreAsInt;
            }


            /*
            //ORIGINAL NOTES FOR CALULATING A BOWLING GAME SCORE 
            if (current frame throw1 == X)
            then
                score = 10 + add next two throws (regardless of the frame)
            if else (current frame throw1 != X && throw2 = /)
            then
                score = 10 + add next throw
             else
             then
                score = throw1 + throw2 (will be less than 10)
             */

            return score;
        }
    }
}
