using System.Collections.Generic;
using System.Linq;

namespace AnalysisVerificationLab1.IfBlocksParser
{
    public class IfParser
    {
        //Find IF in graph and create type for his children
        public static Dictionary<int, Dictionary<int, string>> CreateIfChildrenType(
            Dictionary<int, List<int>> connections, Dictionary<int, string> linesDictionary)
        {
            var result = new Dictionary<int, Dictionary<int, string>>();
            foreach (var (key, value) in linesDictionary)
            {
                // find IF
                if (value.Length > 3 && value[..3].Equals("if "))
                {
                    // find current IF children
                    var children = connections[key];

                    result[key] = new Dictionary<int, string>();
                    // create types for children
                    foreach (var child in children)
                    {
                        switch (linesDictionary[child])
                        {
                            // then
                            case "Block":
                                result[key].Add(child, "then");
                                break;

                            //else
                            case "else":
                                result[key].Add(child, "else");
                                break;

                            //next
                            default:
                                result[key].Add(child, "next");
                                break;
                        }
                    }
                }
            }

            return result;
        }

        // Create new connections with if-then-else
        public static Dictionary<int, List<int>> CreateNewConnectionsWithIfTypes(
            Dictionary<int, List<int>> connections, Dictionary<int, string> linesDictionary,
            Dictionary<int, Dictionary<int, string>> ifTypesDictionary)
        {
            var result = new Dictionary<int, List<int>>(connections);

            //var keysWithNextWithElse = new List<int>();
            var keysWithNextWithoutElse = new Dictionary<int, int>();
            var connectionsToDeleteAndCreateNew = new Dictionary<int, int>();
            var ifIdValues = new List<int>();

            foreach (var (ifId, children) in ifTypesDictionary)
            {
                ifIdValues.Add(ifId);

                if (children.Values.Contains("next"))
                {
                    if (children.Values.Contains("else"))
                    {
                        var keyWithElse = children.SingleOrDefault(x => x.Value.Equals("next")).Key;
                        connectionsToDeleteAndCreateNew.Add(ifId, keyWithElse);
                    }
                    else
                    {
                        var keyWithoutElse = children.SingleOrDefault(x => x.Value.Equals("next")).Key;
                        keysWithNextWithoutElse.Add(ifId, keyWithoutElse);
                    }
                }
            }

            // delete connections
            foreach (var (key, value) in connectionsToDeleteAndCreateNew)
            {
                result.SingleOrDefault(x => x.Key == key).Value.Remove(value);
            }

            ifIdValues.Reverse();
            var lastKeysWithoutConnectionsFromIf = new Dictionary<int, List<int>>();

            foreach (var ifId in ifIdValues)
            {
                // Find last blocks without connections
                var currentNode = connections.SingleOrDefault(x => x.Key == ifId);
                var lastKeys = FindLastKeys(new List<int>(), result, currentNode, linesDictionary).Distinct().ToList();
                lastKeysWithoutConnectionsFromIf[ifId] = lastKeys;

                // Add connections to last Blocks without connections
                var currentLastKeys = lastKeysWithoutConnectionsFromIf[ifId];
                var keyFromDelBlock = connectionsToDeleteAndCreateNew.SingleOrDefault(x => x.Key == ifId).Value;
                if (keyFromDelBlock != 0)
                {
                    foreach (var key in currentLastKeys)
                    {
                        result[key] = new List<int> { keyFromDelBlock };
                    }
                }

                // Add connections to last Blocks with connections
                var keyWithNextWithoutElse = keysWithNextWithoutElse.SingleOrDefault(x => x.Key == ifId).Value;
                // find next block in if-branch
                if (keyWithNextWithoutElse != 0)
                {
                    foreach (var key in currentLastKeys)
                    {
                        if (key != keyWithNextWithoutElse)
                        {
                            result[key] = new List<int> { keyWithNextWithoutElse };
                        }
                    }
                }
                // find next block in for-branch
                /*else if (incrementBlocksDictionary.Count != 0)
                {
                    var forKeysList = incrementBlocksDictionary.Keys.ToList();
                    forKeysList.Reverse();
                    foreach (var key in currentLastKeys)
                    {
                        foreach (var forKey in forKeysList)
                        {
                            if (key > forKey)
                            {
                                result[key] = new List<int> { forKey };
                                break;
                            }
                        }
                    }
                }*/
            }

            return result;
        }

        private static List<int> FindLastKeys(List<int> curRes, Dictionary<int, List<int>> connections,
            KeyValuePair<int, List<int>> currentNode, Dictionary<int, string> linesDictionary)
        {
            // TODO - fix bag Test7 - mb, we haven't got point to next
            var xx = 0;
            // ignore For-block
            if (linesDictionary[currentNode.Key].Length == 5 & linesDictionary[currentNode.Key].Equals("Block"))
            {
                var preNodeKey = connections.SingleOrDefault(x => x.Value.Contains(currentNode.Key)).Key;
                var preNodeValue = linesDictionary[preNodeKey];
                if (preNodeValue.Length > 4 & preNodeValue[..4].Equals("for;"))
                {
                    return curRes;
                }
            }

            foreach (var value in currentNode.Value)
            {
                if (!connections.ContainsKey(value))
                {
                    var linesValue = linesDictionary[value];

                    // ignore Return-block
                    if (linesValue.Length > 7 && linesValue[..7].Equals("return "))
                    {
                    }
                    else
                    {
                        curRes.Add(value);
                    }
                }
                else
                {
                    var newNode = connections.SingleOrDefault(x => x.Key == value);
                    FindLastKeys(curRes, connections, newNode, linesDictionary);
                }
            }

            return curRes;
        }

        // Create if-labels
        public static Dictionary<KeyValuePair<int, int>, string> CreateIfLabelsDictionary(
            Dictionary<int, List<int>> connections, Dictionary<int, string> linesDictionary)
        {
            var result = new Dictionary<KeyValuePair<int, int>, string>();
            var ifDictionary = new Dictionary<int, List<int>>();

            foreach (var (key, value) in linesDictionary)
            {
                if (value.Length > 3 && value[..3].Equals("if "))
                {
                    ifDictionary[key] = connections[key];
                }
            }

            var lastValues = new List<int>();

            foreach (var (key, values) in ifDictionary)
            {
                if (values.Count != 2)
                {
                    var truePair = new KeyValuePair<int, int>(key, values[0]);
                    result[truePair] = "true";

                    // find Else Block
                    var newConnect = lastValues.Max();
                    connections[key].Add(newConnect);

                    var falsePair = new KeyValuePair<int, int>(key, newConnect);
                    result[falsePair] = "false";
                }
                else
                {
                    var valueMin = values.Min();
                    var valueMax = values.Max();

                    var minPair = new KeyValuePair<int, int>(key, valueMin);
                    var maxPair = new KeyValuePair<int, int>(key, valueMax);

                    result[minPair] = "true";
                    result[maxPair] = "false";
                }

                lastValues = values;
            }

            return result;
        }

        // Delete Block from If with letters "Block", "else"
        public static Dictionary<int, List<int>> DeleteIfBlocks(Dictionary<int, List<int>> connections,
            Dictionary<int, string> linesDictionary, Dictionary<KeyValuePair<int, int>, string> labelsDictionary)
        {
            var result = new Dictionary<int, List<int>>(connections);
            var ifDictionary = new Dictionary<int, List<int>>();

            foreach (var (key, value) in linesDictionary)
            {
                if (value.Length > 3 && value[..3].Equals("if "))
                {
                    ifDictionary[key] = connections[key];
                }
            }

            var valuesToDeleteThen = new Dictionary<int, int>();
            var valuesToAddThen = new Dictionary<int, int>();

            var valuesToDeleteElse = new List<KeyValuePair<int, int>>();
            var valuesToAddElse = new Dictionary<int, int>();

            foreach (var (key, values) in ifDictionary)
            {
                foreach (var value in values)
                {
                    var curValue = linesDictionary[value];
                    switch (curValue.Length)
                    {
                        case 5 when curValue.Equals("Block"):
                        {
                            var nextNode = result[value][0];
                            valuesToAddThen[key] = nextNode;
                            valuesToDeleteThen[key] = value;
                            break;
                        }
                        case 4 when curValue.Equals("else"):
                        {
                            var nextBlock = result[value][0];
                            var first = new KeyValuePair<int, int>(key, value);

                            var nextNode = result[nextBlock][0];
                            valuesToAddElse[key] = nextNode;
                            var second = new KeyValuePair<int, int>(value, nextBlock);

                            valuesToDeleteElse.Add(first);
                            valuesToDeleteElse.Add(second);
                            break;
                        }
                    }
                }
            }

            foreach (var (key, value) in valuesToAddThen)
            {
                result[key].Add(value);
            }

            foreach (var (key, value) in valuesToDeleteThen)
            {
                var curPair = new KeyValuePair<int, int>(key, value);
                var tryGetValuePair = labelsDictionary.TryGetValue(curPair, out string stringValue);
                if (tryGetValuePair)
                {
                    var newValue = valuesToAddThen.SingleOrDefault(x => x.Key == key);
                    labelsDictionary.Remove(curPair);
                    labelsDictionary[newValue] = stringValue;
                }

                result.Remove(value);
                result.SingleOrDefault(x => x.Key == key).Value.Remove(value);
                linesDictionary.Remove(value);
            }

            foreach (var (key, value) in valuesToAddElse)
            {
                result[key].Add(value);
            }

            for (var i = 0; i < valuesToDeleteElse.Count; i += 2)
            {
                var curValueFirst = valuesToDeleteElse[i];
                var curValueSecond = valuesToDeleteElse[i + 1];

                var curPair = new KeyValuePair<int, int>(curValueFirst.Key, curValueFirst.Value);
                var tryGetValuePair = labelsDictionary.TryGetValue(curPair, out string stringValue);
                if (tryGetValuePair)
                {
                    var newValue = valuesToAddElse.SingleOrDefault(x => x.Key == curValueFirst.Key);
                    labelsDictionary.Remove(curPair);
                    labelsDictionary[newValue] = stringValue;
                }

                result.Remove(curValueFirst.Value);
                result.SingleOrDefault(x => x.Key == curValueFirst.Key).Value.Remove(curValueFirst.Value);
                linesDictionary.Remove(curValueFirst.Value);

                result.Remove(curValueSecond.Value);
                linesDictionary.Remove(curValueSecond.Value);
            }

            return result;
        }
    }
}